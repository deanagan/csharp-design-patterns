namespace Command
{
    public class ClearCommand : ICommand
    {
        private IProductList _productList;
        public ClearCommand(IProductList productList)
        {
            _productList = productList;
        }
        public void Execute()
        {
            _productList.Products.Clear();
        }
    }
}