using System;

namespace Builder;

public class CommercialLoanBuilder : ILoanApplicationBuilder
{
    public const decimal MinimumCommercialAmount = 25000m;
    public const decimal MinimumCollateralCoverageRatio = 0.75m;
    public const int MaximumTermMonths = 120;

    private string? _applicationId;
    private Borrower? _borrower;
    private decimal _principalAmount;
    private int _termInMonths;
    private decimal _interestRate;
    private decimal _originationFee;
    private RepaymentFrequency _frequency = RepaymentFrequency.Monthly;
    private Collateral? _collateral;
    private string? _businessRegistrationNumber;
    private bool _requiresGuarantor;

    public ILoanApplicationBuilder SetApplicationId(string applicationId)
    {
        _applicationId = applicationId;
        return this;
    }

    public ILoanApplicationBuilder SetBorrower(string fullName, int creditScore, decimal annualIncome)
    {
        _borrower = new Borrower(fullName, creditScore, annualIncome);
        return this;
    }

    public ILoanApplicationBuilder SetLoanTerms(decimal principalAmount, int termInMonths, decimal interestRate)
    {
        _principalAmount = principalAmount;
        _termInMonths = termInMonths;
        _interestRate = interestRate;
        return this;
    }

    public ILoanApplicationBuilder SetRepaymentFrequency(RepaymentFrequency frequency)
    {
        _frequency = frequency;
        return this;
    }

    public ILoanApplicationBuilder SetOriginationFee(decimal feeAmount)
    {
        _originationFee = feeAmount;
        return this;
    }

    public ILoanApplicationBuilder SetCollateral(string assetType, decimal estimatedValue)
    {
        _collateral = new Collateral(assetType, estimatedValue);
        return this;
    }

    public ILoanApplicationBuilder SetBusinessRegistration(string registrationNumber)
    {
        _businessRegistrationNumber = registrationNumber;
        return this;
    }

    public ILoanApplicationBuilder SetGuarantorRequired(bool isRequired)
    {
        _requiresGuarantor = isRequired;
        return this;
    }

    public LoanApplication Build()
    {
        if (string.IsNullOrWhiteSpace(_applicationId))
        {
            throw new InvalidOperationException("ApplicationId is required for commercial facilities.");
        }

        if (_borrower == null)
        {
            throw new InvalidOperationException("Principal officer or borrower details are required.");
        }

        if (string.IsNullOrWhiteSpace(_businessRegistrationNumber))
        {
            throw new InvalidOperationException("Business registration number is mandatory for commercial loans.");
        }

        if (_principalAmount < MinimumCommercialAmount)
        {
            throw new InvalidOperationException(
                $"Commercial facilities require a minimum amount of ${MinimumCommercialAmount:N2}.");
        }

        if (_termInMonths <= 0 || _termInMonths > MaximumTermMonths)
        {
            throw new InvalidOperationException(
                $"Term must be between 1 and {MaximumTermMonths} months for commercial loans.");
        }

        if (_interestRate <= 0)
        {
            throw new InvalidOperationException("Interest rate must be greater than zero.");
        }

        if (_collateral == null)
        {
            throw new InvalidOperationException("Commercial loans require registered collateral.");
        }

        var requiredCollateral = _principalAmount * MinimumCollateralCoverageRatio;
        if (_collateral.EstimatedValue < requiredCollateral)
        {
            throw new InvalidOperationException(
                $"Collateral value of ${_collateral.EstimatedValue:N2} does not satisfy the {MinimumCollateralCoverageRatio:P0} coverage requirement (${requiredCollateral:N2}).");
        }

        return new LoanApplication
        {
            ApplicationId = _applicationId,
            ProductType = LoanType.Commercial,
            Borrower = _borrower,
            PrincipalAmount = _principalAmount,
            TermInMonths = _termInMonths,
            InterestRate = _interestRate,
            OriginationFee = _originationFee,
            Frequency = _frequency,
            Collateral = _collateral,
            BusinessRegistrationNumber = _businessRegistrationNumber,
            RequiresGuarantor = _requiresGuarantor
        };
    }
}

