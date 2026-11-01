using System;
using Proxy;
using Shouldly;
using Xunit;

namespace ProxyTest;

public class ProxyTest
{
    private const string LoanId = "LN-1001";
    private const string BorrowerId = "BORROWER-1";

    [Fact]
    public void AllowsBorrowerToViewTheirOwnLoan()
    {
        var proxy = CreateProxy(LoanUserRole.Borrower, BorrowerId, out _);

        var account = proxy.GetAccount(LoanId);

        account.ShouldNotBeNull();
        account.LoanId.ShouldBe(LoanId);
    }

    [Fact]
    public void HidesLoanFromAnotherBorrower()
    {
        var proxy = CreateProxy(LoanUserRole.Borrower, "BORROWER-2", out _);

        proxy.GetAccount(LoanId).ShouldBeNull();
    }

    [Fact]
    public void AllowsLoanOfficerToViewLoanButNotChangeRepaymentInstructions()
    {
        var proxy = CreateProxy(LoanUserRole.LoanOfficer, "OFFICER-1", out var service);

        proxy.GetAccount(LoanId).ShouldNotBeNull();
        proxy.UpdateRepaymentInstructions(LoanId, "New bank account").ShouldBeFalse();
        service.GetAccount(LoanId)!.RepaymentInstructions.ShouldBe("Existing instructions");
    }

    [Fact]
    public void AllowsBorrowerToUpdateTheirOwnRepaymentInstructions()
    {
        var proxy = CreateProxy(LoanUserRole.Borrower, BorrowerId, out var service);

        proxy.UpdateRepaymentInstructions(LoanId, "Updated bank account").ShouldBeTrue();

        service.GetAccount(LoanId)!.RepaymentInstructions.ShouldBe("Updated bank account");
    }

    [Fact]
    public void DeniesBorrowerUpdatingAnotherBorrowersRepaymentInstructions()
    {
        var proxy = CreateProxy(LoanUserRole.Borrower, "BORROWER-2", out var service);

        proxy.UpdateRepaymentInstructions(LoanId, "Unauthorized change").ShouldBeFalse();
        service.GetAccount(LoanId)!.RepaymentInstructions.ShouldBe("Existing instructions");
    }

    [Fact]
    public void DeniesPayoutUnlessUserIsServicingAdministrator()
    {
        var borrowerProxy = CreateProxy(LoanUserRole.Borrower, BorrowerId, out var borrowerService);
        var officerProxy = CreateProxy(LoanUserRole.LoanOfficer, "OFFICER-1", out var officerService);

        borrowerProxy.InitiatePayout(LoanId, 5_000m).ShouldBeFalse();
        officerProxy.InitiatePayout(LoanId, 5_000m).ShouldBeFalse();
        borrowerService.GetAccount(LoanId)!.OutstandingBalance.ShouldBe(80_000m);
        officerService.GetAccount(LoanId)!.OutstandingBalance.ShouldBe(80_000m);
    }

    [Fact]
    public void AllowsServicingAdministratorToManageLoanAndInitiatePayout()
    {
        var proxy = CreateProxy(
            LoanUserRole.ServicingAdministrator,
            "ADMIN-1",
            out var service);

        proxy.UpdateRepaymentInstructions(LoanId, "Administrator update").ShouldBeTrue();
        proxy.InitiatePayout(LoanId, 5_000m).ShouldBeTrue();

        service.GetAccount(LoanId)!.RepaymentInstructions.ShouldBe("Administrator update");
        service.GetAccount(LoanId)!.OutstandingBalance.ShouldBe(75_000m);
    }

    [Fact]
    public void ReturnsNotFoundForUnknownLoan()
    {
        var proxy = CreateProxy(LoanUserRole.ServicingAdministrator, "ADMIN-1", out _);

        proxy.GetAccount("MISSING").ShouldBeNull();
        proxy.UpdateRepaymentInstructions("MISSING", "Instructions").ShouldBeFalse();
        proxy.InitiatePayout("MISSING", 1_000m).ShouldBeFalse();
    }

    [Fact]
    public void RejectsNullDependencies()
    {
        var service = CreateService();
        var user = new LoanUserContext("ADMIN-1", LoanUserRole.ServicingAdministrator);

        Should.Throw<ArgumentNullException>(() => new LoanAccountAuthorizationProxy(null!, service));
        Should.Throw<ArgumentNullException>(() => new LoanAccountAuthorizationProxy(user, null!));
    }

    private static LoanAccountAuthorizationProxy CreateProxy(
        LoanUserRole role,
        string userId,
        out LoanAccountService service)
    {
        service = CreateService();
        return new LoanAccountAuthorizationProxy(
            new LoanUserContext(userId, role),
            service);
    }

    private static LoanAccountService CreateService() =>
        new(
        [
            new LoanAccount(
                LoanId,
                BorrowerId,
                OutstandingBalance: 80_000m,
                RepaymentInstructions: "Existing instructions")
        ]);
}
