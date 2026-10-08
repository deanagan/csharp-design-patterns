using System;

namespace Builder;

public class PersonalLoanBuilder : ILoanApplicationBuilder
{
    public const decimal MaximumUnsecuredAmount = 50000m;
    public const int MinimumCreditScore = 600;
    public const int MaximumTermMonths = 60;

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
            throw new InvalidOperationException("ApplicationId is required for personal loans.");
        }

        if (_borrower == null)
        {
            throw new InvalidOperationException("Borrower details are required.");
        }

        if (_borrower.CreditScore < MinimumCreditScore)
        {
            throw new InvalidOperationException(
                $"Borrower credit score of {_borrower.CreditScore} is below the personal loan minimum of {MinimumCreditScore}.");
        }

        if (_principalAmount <= 0)
        {
            throw new InvalidOperationException("Principal amount must be greater than zero.");
        }

        if (_principalAmount > MaximumUnsecuredAmount)
        {
            throw new InvalidOperationException(
                $"Principal amount of ${_principalAmount:N2} exceeds the maximum unsecured limit of ${MaximumUnsecuredAmount:N2}.");
        }

        if (_termInMonths <= 0 || _termInMonths > MaximumTermMonths)
        {
            throw new InvalidOperationException(
                $"Term must be between 1 and {MaximumTermMonths} months for personal loans.");
        }

        if (_interestRate <= 0)
        {
            throw new InvalidOperationException("Interest rate must be greater than zero.");
        }

        if (!string.IsNullOrWhiteSpace(_businessRegistrationNumber))
        {
            throw new InvalidOperationException("Personal loans cannot have a business registration number.");
        }

        return new LoanApplication
        {
            ApplicationId = _applicationId,
            ProductType = LoanType.Personal,
            Borrower = _borrower,
            PrincipalAmount = _principalAmount,
            TermInMonths = _termInMonths,
            InterestRate = _interestRate,
            OriginationFee = _originationFee,
            Frequency = _frequency,
            Collateral = _collateral,
            BusinessRegistrationNumber = null,
            RequiresGuarantor = _requiresGuarantor
        };
    }
}

