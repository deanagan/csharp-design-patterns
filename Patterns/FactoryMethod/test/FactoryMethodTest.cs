using FactoryMethod;
using Shouldly;
using System;
using Xunit;

namespace FactoryMethodTest;

public class BusinessLoanProcessorFactoryTests
{
    [Theory]
    [InlineData(BusinessLoanType.MerchantCashAdvance, typeof(MerchantCashAdvanceProcessor))]
    [InlineData(BusinessLoanType.InvoiceFactoring, typeof(InvoiceFactoringProcessor))]
    [InlineData(BusinessLoanType.EquipmentTermLoan, typeof(EquipmentTermLoanProcessor))]
    public void Create_ReturnsExpectedProcessor_ForGivenLoanType(BusinessLoanType loanType, Type expectedProcessorType)
    {
        // Act
        var processor = BusinessLoanProcessorFactory.Create(loanType);

        // Assert
        processor.ShouldNotBeNull();
        processor.ShouldBeOfType(expectedProcessorType);
    }

    [Fact]
    public void Create_ThrowsNotSupportedException_ForInvalidLoanType()
    {
        // Act & Assert
        Should.Throw<NotSupportedException>(() => 
            BusinessLoanProcessorFactory.Create((BusinessLoanType)999));
    }

    [Fact]
    public void MerchantCashAdvanceProcessor_CalculatesCorrectLimitAndDescription()
    {
        // Arrange
        var processor = new MerchantCashAdvanceProcessor();
        var profile = new BusinessProfile { AverageMonthlyCardSales = 10000M };

        // Act
        var limit = processor.CalculateMaxCreditLimit(profile);
        var description = processor.GetRepaymentStructureDescription();

        // Assert
        limit.ShouldBe(15000M); // 10,000 * 1.5
        description.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public void InvoiceFactoringProcessor_CalculatesCorrectLimitAndDescription()
    {
        // Arrange
        var processor = new InvoiceFactoringProcessor();
        var profile = new BusinessProfile { TotalOutstandingInvoices = 50000M };

        // Act
        var limit = processor.CalculateMaxCreditLimit(profile);
        var description = processor.GetRepaymentStructureDescription();

        // Assert
        limit.ShouldBe(40000M); // 50,000 * 0.80
        description.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public void EquipmentTermLoanProcessor_CalculatesCorrectLimitAndDescription()
    {
        // Arrange
        var processor = new EquipmentTermLoanProcessor();
        var profile = new BusinessProfile { AppraisedEquipmentValue = 100000M };

        // Act
        var limit = processor.CalculateMaxCreditLimit(profile);
        var description = processor.GetRepaymentStructureDescription();

        // Assert
        limit.ShouldBe(90000M); // 100,000 * 0.90
        description.ShouldNotBeNullOrWhiteSpace();
    }
}