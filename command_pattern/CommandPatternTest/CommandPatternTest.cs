using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using FluentAssertions;

namespace Command.Test
{
    public class CommandPatternShould
    {
        private IProductList _ProductList;
        private IInvoker _productInvoker;

        public CommandPatternShould()
        {
            _ProductList = Mock.Of<IProductList>
            (
                productList =>
                    productList.Name == "WishList" &&
                    productList.Products == (new List<IProduct>())
            );

            _productInvoker = new ProductInvoker();
        }

        public static IEnumerable<object[]> GetProducts()
        {
            yield return new object[] {
                Mock.Of<IProduct>
                (
                    product =>
                        product.Name == "Product 1" &&
                        product.Price == 45.00M
                ),
                Mock.Of<IProduct>
                (
                    product =>
                        product.Name == "Product 2" &&
                        product.Price == 55.00M
                )
            };
        }


        [Theory]
        [MemberData(nameof(GetProducts))]
        public void AddItems_WhenUsingAddCommand(IProduct product1, IProduct product2)
        {
            // Arrange
            var addProduct1Command = new AddCommand(_ProductList, product1);
            var addProduct2Command = new AddCommand(_ProductList, product2);

            // Act
            _productInvoker.AddCommand("addproduct1", addProduct1Command);
            _productInvoker.AddCommand("addproduct2", addProduct2Command);
            _productInvoker.InvokeCommand("addproduct1");
            _productInvoker.InvokeCommand("addproduct2");

            // Assert
            _ProductList.Products.Should().NotBeNullOrEmpty()
                                          .And
                                          .HaveCount(2)
                                          .And
                                          .OnlyHaveUniqueItems()
                                          .And
                                          .Contain(product1)
                                          .And
                                          .Contain(product2);
        }

        [Theory]
        [MemberData(nameof(GetProducts))]
        public void AddTwoRemoveOneCorrectly_WhenUsingRemoveCommand(IProduct product1, IProduct product2)
        {
            // Arrange
            var addProduct1Command = new AddCommand(_ProductList, product1);
            _productInvoker.AddCommand("addproduct1", addProduct1Command);

            var addProduct2Command = new AddCommand(_ProductList, product2);
            _productInvoker.AddCommand("addproduct2", addProduct2Command);

            var removeProduct2Command = new RemoveCommand(_ProductList, product2);
            _productInvoker.AddCommand("removeproduct2", removeProduct2Command);

            // Act
            _productInvoker.InvokeCommand("addproduct1");
            _productInvoker.InvokeCommand("addproduct2");
            _productInvoker.InvokeCommand("removeproduct2");

            // Assert
            _ProductList.Products.Should().NotBeNullOrEmpty()
                                          .And
                                          .HaveCount(1)
                                          .And
                                          .OnlyHaveUniqueItems()
                                          .And
                                          .NotContain(product2)
                                          .And
                                          .Contain(product1);
        }

        [Theory]
        [MemberData(nameof(GetProducts))]
        public void ClearItems_WhenUsingClearCommand(IProduct product1, IProduct product2)
        {
            // Arrange
            var addProduct1Command = new AddCommand(_ProductList, product1);
            _productInvoker.AddCommand("addproduct1", addProduct1Command);

            var addProduct2Command = new AddCommand(_ProductList, product2);
            _productInvoker.AddCommand("addproduct2", addProduct2Command);

            var clearCommand = new ClearCommand(_ProductList);
            _productInvoker.AddCommand("clearall", clearCommand);

            // Act
            _productInvoker.InvokeCommand("addproduct1");
            _productInvoker.InvokeCommand("addproduct2");
            _productInvoker.InvokeCommand("clearall");

            // Assert
            _ProductList.Products.Should().BeEmpty();
        }
    }
}
