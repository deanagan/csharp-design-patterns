namespace Command
{
    public class AddCommand : ICommand
    {
        private IProduct _product;
        private IProductList _productList;

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
