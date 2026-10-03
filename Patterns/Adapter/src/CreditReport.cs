namespace Adapter;

public record CreditReport 
{
    public required string ApplicantId;
    public int NormalizedScore; // Standardized 0-1000 scale
    public bool HasDefaults;
    public required string RiskTier;
}