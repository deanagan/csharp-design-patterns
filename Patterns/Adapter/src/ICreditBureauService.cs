using System.Threading;
using System.Threading.Tasks;

namespace Adapter;

public interface ICreditBureauService
{
    Task<CreditReport> GetCreditReportAsync(string applicantId, CancellationToken cancellationToken);
}