namespace Command
{
    public class AddCommand : ICommand
    {
        private IProductList _productList;
        private IProduct _product;
        public AddCommand(IProductList productList, IProduct product)
        {
            _productList = productList;
            _product = product;
        }
        public void Execute()
        {
            _productList.Products.Add(_product);
        }
    }
}