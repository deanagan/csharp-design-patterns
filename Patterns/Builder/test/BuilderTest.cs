using System;
using Builder;
using Shouldly;
using Xunit;

namespace Builder.Test;

public class BuilderShould
{
    private readonly LoanUnderwritingDirector _director = new();

    [Fact]
    public void Director_ShouldConstruct_StandardPersonalLoan()
    {
        // Arrange
        var builder = new PersonalLoanBuilder();
        const string appId = "PL-1001";
        const string borrower = "Jane Doe";
        const int creditScore = 740;
        const decimal income = 95000m;
        const decimal amount = 20000m;

        // Act
        var loan = _director.ConstructStandardPersonalLoan(
            builder, appId, borrower, creditScore, income, amount);

        // Assert
        loan.ShouldNotBeNull();
        loan.ApplicationId.ShouldBe(appId);
        loan.ProductType.ShouldBe(LoanType.Personal);
        loan.Borrower.FullName.ShouldBe(borrower);
        loan.Borrower.CreditScore.ShouldBe(creditScore);
        loan.Borrower.AnnualIncome.ShouldBe(income);
        loan.PrincipalAmount.ShouldBe(amount);
        loan.TermInMonths.ShouldBe(36);
        loan.InterestRate.ShouldBe(0.089m);
        loan.Frequency.ShouldBe(RepaymentFrequency.Monthly);
        loan.OriginationFee.ShouldBe(amount * 0.015m); // $300
        loan.BusinessRegistrationNumber.ShouldBeNull();
        loan.Collateral.ShouldBeNull();

        // Financial calculations
        loan.TotalInterestPayable.ShouldBe(5340.00m); // 20000 * 0.089 * 3
        loan.TotalRepaymentAmount.ShouldBe(25640.00m); // 20000 + 5340 + 300
        loan.MonthlyPayment.ShouldBe(712.22m); // 25640 / 36
    }

    [Fact]
    public void Director_ShouldConstruct_SecuredCommercialFacility()
    {
        // Arrange
        var builder = new CommercialLoanBuilder();
        const string appId = "CL-9002";
        const string owner = "Acme Corp (Rep: John Smith)";
        const string businessReg = "ACN-123-456-789";
        const decimal revenue = 1200000m;
        const decimal amount = 250000m;
        const string collateralAsset = "Commercial Warehouse - Unit 4";
        const decimal collateralValue = 350000m;

        // Act
        var facility = _director.ConstructSecuredCommercialFacility(
            builder, appId, owner, businessReg, revenue, amount, collateralAsset, collateralValue);

        // Assert
        facility.ShouldNotBeNull();
        facility.ApplicationId.ShouldBe(appId);
        facility.ProductType.ShouldBe(LoanType.Commercial);
        facility.BusinessRegistrationNumber.ShouldBe(businessReg);
        facility.PrincipalAmount.ShouldBe(amount);
        facility.TermInMonths.ShouldBe(60);
        facility.InterestRate.ShouldBe(0.065m);
        facility.Collateral.ShouldNotBeNull();
        facility.Collateral.AssetType.ShouldBe(collateralAsset);
        facility.Collateral.EstimatedValue.ShouldBe(collateralValue);
        facility.OriginationFee.ShouldBe(amount * 0.01m); // $2,500

        // Financial calculations
        facility.TotalInterestPayable.ShouldBe(81250.00m); // 250000 * 0.065 * 5
        facility.TotalRepaymentAmount.ShouldBe(333750.00m); // 250000 + 81250 + 2500
        facility.MonthlyPayment.ShouldBe(5562.50m); // 333750 / 60
    }

    [Fact]
    public void PersonalLoanBuilder_ShouldConstruct_CustomLoanDirectly()
    {
        // Arrange
        var builder = new PersonalLoanBuilder();

        // Act
        var customLoan = builder
            .SetApplicationId("PL-CUSTOM-88")
            .SetBorrower("Robert Taylor", 690, 80000m)
            .SetLoanTerms(12000m, termInMonths: 24, interestRate: 0.075m)
            .SetRepaymentFrequency(RepaymentFrequency.BiWeekly)
            .SetOriginationFee(150m)
            .SetGuarantorRequired(true)
            .Build();

        // Assert
        customLoan.ShouldNotBeNull();
        customLoan.ApplicationId.ShouldBe("PL-CUSTOM-88");
        customLoan.Frequency.ShouldBe(RepaymentFrequency.BiWeekly);
        customLoan.RequiresGuarantor.ShouldBeTrue();
        customLoan.PrincipalAmount.ShouldBe(12000m);
        customLoan.TotalInterestPayable.ShouldBe(1800.00m); // 12000 * 0.075 * 2
        customLoan.TotalRepaymentAmount.ShouldBe(13950.00m); // 12000 + 1800 + 150
    }

    [Fact]
    public void PersonalLoanBuilder_ShouldThrow_WhenPrincipalExceedsUnsecuredLimit()
    {
        // Arrange
        var builder = new PersonalLoanBuilder()
            .SetApplicationId("PL-EXCEED")
            .SetBorrower("Alice Brown", 750, 150000m)
            .SetLoanTerms(PersonalLoanBuilder.MaximumUnsecuredAmount + 1m, 36, 0.08m);

        // Act & Assert
        var ex = Should.Throw<InvalidOperationException>(() => builder.Build());
        ex.Message.ShouldContain("exceeds the maximum unsecured limit");
    }

    [Fact]
    public void PersonalLoanBuilder_ShouldThrow_WhenCreditScoreIsBelowMinimum()
    {
        // Arrange
        var builder = new PersonalLoanBuilder()
            .SetApplicationId("PL-LOW-SCORE")
            .SetBorrower("Bob Low", creditScore: 550, annualIncome: 60000m)
            .SetLoanTerms(10000m, 24, 0.09m);

        // Act & Assert
        var ex = Should.Throw<InvalidOperationException>(() => builder.Build());
        ex.Message.ShouldContain("below the personal loan minimum");
    }

    [Fact]
    public void PersonalLoanBuilder_ShouldThrow_WhenBusinessRegistrationIsProvided()
    {
        // Arrange
        var builder = new PersonalLoanBuilder()
            .SetApplicationId("PL-INVALID-REG")
            .SetBorrower("Charlie Cox", 710, 85000m)
            .SetLoanTerms(15000m, 36, 0.08m)
            .SetBusinessRegistration("ACN-999-999");

        // Act & Assert
        var ex = Should.Throw<InvalidOperationException>(() => builder.Build());
        ex.Message.ShouldContain("cannot have a business registration number");
    }

    [Fact]
    public void CommercialLoanBuilder_ShouldThrow_WhenBusinessRegistrationIsMissing()
    {
        // Arrange
        var builder = new CommercialLoanBuilder()
            .SetApplicationId("CL-NO-REG")
            .SetBorrower("David Miller", 730, 300000m)
            .SetLoanTerms(100000m, 48, 0.06m)
            .SetCollateral("Machinery", 150000m);

        // Act & Assert
        var ex = Should.Throw<InvalidOperationException>(() => builder.Build());
        ex.Message.ShouldContain("Business registration number is mandatory");
    }

    [Fact]
    public void CommercialLoanBuilder_ShouldThrow_WhenCollateralIsMissing()
    {
        // Arrange
        var builder = new CommercialLoanBuilder()
            .SetApplicationId("CL-NO-COLLATERAL")
            .SetBorrower("David Miller", 730, 300000m)
            .SetBusinessRegistration("ACN-111-222")
            .SetLoanTerms(100000m, 48, 0.06m);

        // Act & Assert
        var ex = Should.Throw<InvalidOperationException>(() => builder.Build());
        ex.Message.ShouldContain("Commercial loans require registered collateral");
    }

    [Fact]
    public void CommercialLoanBuilder_ShouldThrow_WhenCollateralIsInsufficient()
    {
        // Arrange (Loan of $100,000 requires at least 75% collateral = $75,000)
        var builder = new CommercialLoanBuilder()
            .SetApplicationId("CL-LOW-COLLATERAL")
            .SetBorrower("David Miller", 730, 300000m)
            .SetBusinessRegistration("ACN-111-222")
            .SetLoanTerms(100000m, 48, 0.06m)
            .SetCollateral("Delivery Van", 50000m); // Only 50% coverage

        // Act & Assert
        var ex = Should.Throw<InvalidOperationException>(() => builder.Build());
        ex.Message.ShouldContain("does not satisfy the 75% coverage requirement");
    }
}
