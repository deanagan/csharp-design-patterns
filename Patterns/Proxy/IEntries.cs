
namespace Proxy
{
    public interface IEntries
    {
        bool Delete(int id);
        IProductInfo? Get(int id);
    }
}
