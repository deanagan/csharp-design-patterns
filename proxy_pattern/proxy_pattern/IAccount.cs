using System;

namespace ProxyPattern
{
    public interface IAccount
    {
        bool Delete(int id);
        IProductInfo Get(int id);
    }
}
