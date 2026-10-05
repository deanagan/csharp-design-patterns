namespace Proxy
{
    public class EntriesProxy : IEntries
    {
        private IAuthenticationController _authCtrl;
        private IEntries _realEntries;
        public EntriesProxy(IAuthenticationController authCtrl, IEntries realEntries)
        {
            _authCtrl = authCtrl;
            _realEntries = realEntries;
        }

        public bool Delete(int id)
        {
            var result = false;
            if (_authCtrl.IsAdmin())
            {
                result = _realEntries.Delete(id);
            }
            return result;
        }
        public IProductInfo? Get(int id)
        {
            return _realEntries?.Get(id) ?? null;
        }

    }
}
