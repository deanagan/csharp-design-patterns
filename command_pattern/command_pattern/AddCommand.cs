namespace Command
{
    public class AddCommand : ICommand
    {
        private IProductList _productList;
        public AddCommand(IProductList productList)
        {
            _productList = productList;
        }
        public void Execute(IProduct product)
        {
            _productList.Products.Add(product);
        }
    }
}