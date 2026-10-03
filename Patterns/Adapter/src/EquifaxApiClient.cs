using System.Threading.Tasks;

namespace Adapter;

public interface IEquifaxApiClient
{
    Task<string> FetchXmlReportAsync(string id);
}

public class EquifaxApiClient : IEquifaxApiClient
{
    // Simulating a third-party SDK you don't control
    public async Task<string> FetchXmlReportAsync(string id) => "<EquifaxResponse><Score>720</Score><Defaults>false</Defaults></EquifaxResponse>";
}