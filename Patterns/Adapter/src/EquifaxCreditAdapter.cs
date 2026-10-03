using System.Threading;
using System.Threading.Tasks;

namespace Adapter;

public class EquifaxCreditAdapter(IEquifaxApiClient equifaxClient) : ICreditBureauService
{
    public async Task<CreditReport> GetCreditReportAsync(string applicantId, CancellationToken cancellationToken)
    {
        // Translate domain request to vendor format
        string rawXml = await equifaxClient.FetchXmlReportAsync(applicantId);
        
        // (Parsing logic for Equifax XML structure...)
        int equifaxScore = 720; // Extracted from XML
        bool defaults = false;   // Extracted from XML

        // Adapt Equifax's 850 scale to your system's unified scale, map properties
        int normalizedScore = (int)(equifaxScore * (1000.0 / 850.0));
        string tier = normalizedScore > 750 ? "Prime" : "Standard";

        return new CreditReport
        {
            ApplicantId = applicantId,
            NormalizedScore = normalizedScore,
            HasDefaults = defaults,
            RiskTier = tier
        };
    }
}
