using System.Threading.Tasks;

namespace Adapter;

public record ExperianResponseDto(int ScoreValue, bool BankruptcyFound, string RiskCategory);

public interface IExperianApiClient
{
    Task<ExperianResponseDto> GetJsonReportAsync(string id);
}

public class ExperianApiClient : IExperianApiClient
{
    public async Task<ExperianResponseDto> GetJsonReportAsync(string id) => 
        new(880, false, "Tier_A");
}