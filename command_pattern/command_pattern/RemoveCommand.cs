namespace Command
{
    public class RemoveCommand : ICommand
    {
        private IProductList _productList;
        private IProduct _product;
        public RemoveCommand(IProductList productList, IProduct product)
        {
            _productList = productList;
            _product = product;
        }
        public void Execute()
        {
            _productList.Products.Remove(_product);
        }
    }
}