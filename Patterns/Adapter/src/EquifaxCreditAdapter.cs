namespace Adapter;

public class EquifaxCreditAdapter(IEquifaxApiClient equifaxClient) : ICreditBureauService
{
    public async Task<CreditReport> GetCreditReportAsync(string applicantId, CancellationToken cancellationToken)
    {
        // Translate domain request to vendor format
        var rawXml = await equifaxClient.FetchXmlReportAsync(applicantId);
        
        // (Parsing logic for Equifax XML structure...)
        var equifaxScore = 720; // Extracted from XML
        var defaults = false;   // Extracted from XML

        // Adapt Equifax's 850 scale to your system's unified scale, map properties
        var normalizedScore = (int)(equifaxScore * (1000.0 / 850.0));
        var tier = normalizedScore > 750 ? "Prime" : "Standard";

        return new CreditReport
        {
            ApplicantId = applicantId,
            NormalizedScore = normalizedScore,
            HasDefaults = defaults,
            RiskTier = tier
        };
    }
}
