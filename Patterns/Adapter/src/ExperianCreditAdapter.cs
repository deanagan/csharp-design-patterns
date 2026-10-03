using System.Threading;
using System.Threading.Tasks;

namespace Adapter;

public class ExperianCreditAdapter(IExperianApiClient experianClient) : ICreditBureauService
{
    public async Task<CreditReport> GetCreditReportAsync(string applicantId, CancellationToken cancellationToken)
    {
        // Translate domain request to vendor format
        ExperianResponseDto response = await experianClient.GetJsonReportAsync(applicantId);

        // Experian matches your scale natively, just map properties cleanly
        return new CreditReport
        {
            ApplicantId = applicantId,
            NormalizedScore = response.ScoreValue,
            HasDefaults = response.BankruptcyFound,
            RiskTier = response.RiskCategory == "Tier_A" ? "Prime" : "Standard"
        };
    }
}