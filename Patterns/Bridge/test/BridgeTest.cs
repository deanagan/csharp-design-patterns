using System;
using Bridge;
using NSubstitute;
using Shouldly;
using Xunit;

namespace BridgeTest;

public class BridgeShould
{
    private readonly ILedgerBridge _ledgerBridgeMock = Substitute.For<ILedgerBridge>();

    [Fact]
    public void PersonalLoanProduct_ShouldDisburse_ToPersonalLoanAccounts()
    {
        // Arrange
        var loanProduct = new PersonalLoanProduct(_ledgerBridgeMock);
        const string loanId = "PL-1001";
        const decimal amount = 15000.00m;

        _ledgerBridgeMock.RecordEntry(Arg.Any<LedgerTransaction>())
            .Returns(callInfo => new PostingResult("CONF-001", "MockLedger", callInfo.Arg<LedgerTransaction>()));

        // Act
        var result = loanProduct.Disburse(loanId, amount);

        // Assert
        result.ShouldNotBeNull();
        result.ConfirmationCode.ShouldBe("CONF-001");
        result.Transaction.Amount.ShouldBe(amount);

        _ledgerBridgeMock.Received(1).RecordEntry(Arg.Is<LedgerTransaction>(tx =>
            tx.ReferenceId == loanId &&
            tx.Amount == amount &&
            tx.DebitAccount == PersonalLoanProduct.ReceivableAccount &&
            tx.CreditAccount == PersonalLoanProduct.SettlementAccount));
    }

    [Fact]
    public void CommercialLoanProduct_ShouldDisburse_ToCommercialLoanAccounts()
    {
        // Arrange
        var loanProduct = new CommercialLoanProduct(_ledgerBridgeMock);
        const string loanId = "CL-2001";
        const decimal amount = 500000.00m;

        _ledgerBridgeMock.RecordEntry(Arg.Any<LedgerTransaction>())
            .Returns(callInfo => new PostingResult("CONF-002", "MockLedger", callInfo.Arg<LedgerTransaction>()));

        // Act
        var result = loanProduct.Disburse(loanId, amount);

        // Assert
        result.ShouldNotBeNull();
        result.ConfirmationCode.ShouldBe("CONF-002");
        result.Transaction.Amount.ShouldBe(amount);

        _ledgerBridgeMock.Received(1).RecordEntry(Arg.Is<LedgerTransaction>(tx =>
            tx.ReferenceId == loanId &&
            tx.Amount == amount &&
            tx.DebitAccount == CommercialLoanProduct.ReceivableAccount &&
            tx.CreditAccount == CommercialLoanProduct.SettlementAccount));
    }

    [Fact]
    public void PersonalLoanProduct_ShouldApplyRepayment_ToPersonalLoanAccounts()
    {
        // Arrange
        var loanProduct = new PersonalLoanProduct(_ledgerBridgeMock);
        const string loanId = "PL-1001";
        const decimal amount = 750.00m;

        _ledgerBridgeMock.RecordEntry(Arg.Any<LedgerTransaction>())
            .Returns(callInfo => new PostingResult("CONF-003", "MockLedger", callInfo.Arg<LedgerTransaction>()));

        // Act
        var result = loanProduct.ApplyRepayment(loanId, amount);

        // Assert
        result.ShouldNotBeNull();
        result.ConfirmationCode.ShouldBe("CONF-003");
        result.Transaction.Amount.ShouldBe(amount);

        _ledgerBridgeMock.Received(1).RecordEntry(Arg.Is<LedgerTransaction>(tx =>
            tx.ReferenceId == loanId &&
            tx.Amount == amount &&
            tx.DebitAccount == PersonalLoanProduct.SettlementAccount &&
            tx.CreditAccount == PersonalLoanProduct.ReceivableAccount));
    }

    [Fact]
    public void CommercialLoanProduct_ShouldApplyRepayment_ToCommercialLoanAccounts()
    {
        // Arrange
        var loanProduct = new CommercialLoanProduct(_ledgerBridgeMock);
        const string loanId = "CL-2001";
        const decimal amount = 25000.00m;

        _ledgerBridgeMock.RecordEntry(Arg.Any<LedgerTransaction>())
            .Returns(callInfo => new PostingResult("CONF-004", "MockLedger", callInfo.Arg<LedgerTransaction>()));

        // Act
        var result = loanProduct.ApplyRepayment(loanId, amount);

        // Assert
        result.ShouldNotBeNull();
        result.ConfirmationCode.ShouldBe("CONF-004");
        result.Transaction.Amount.ShouldBe(amount);

        _ledgerBridgeMock.Received(1).RecordEntry(Arg.Is<LedgerTransaction>(tx =>
            tx.ReferenceId == loanId &&
            tx.Amount == amount &&
            tx.DebitAccount == CommercialLoanProduct.SettlementAccount &&
            tx.CreditAccount == CommercialLoanProduct.ReceivableAccount));
    }

    public static TheoryData<Func<ILedgerBridge, LoanProduct>, ILedgerBridge, string> ProductAndLedgerCombinations => new()
    {
        { bridge => new PersonalLoanProduct(bridge), new SapErpLedgerBridge(), "SAP-ERP" },
        { bridge => new PersonalLoanProduct(bridge), new CloudJsonLedgerBridge(), "Cloud-Json" },
        { bridge => new CommercialLoanProduct(bridge), new SapErpLedgerBridge(), "SAP-ERP" },
        { bridge => new CommercialLoanProduct(bridge), new CloudJsonLedgerBridge(), "Cloud-Json" }
    };

    [Theory]
    [MemberData(nameof(ProductAndLedgerCombinations))]
    public void SuccessfullyPostTransactionsAcrossBridge_ForAnyLoanProductAndLedgerCombination(
        Func<ILedgerBridge, LoanProduct> productCreator,
        ILedgerBridge ledger,
        string expectedLedgerType)
    {
        // Arrange
        var product = productCreator(ledger);
        const string loanId = "LN-9876";
        const decimal amount = 50000.00m;

        // Act
        var result = product.Disburse(loanId, amount);

        // Assert
        result.ShouldNotBeNull();
        result.LedgerType.ShouldBe(expectedLedgerType);
        result.Transaction.ReferenceId.ShouldBe(loanId);
        result.Transaction.Amount.ShouldBe(amount);
        result.ConfirmationCode.ShouldNotBeNullOrWhiteSpace();
    }
}
