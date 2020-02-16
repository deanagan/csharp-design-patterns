using NUnit.Framework;
using Moq;
using FluentAssertions;
using Command;
using System.Collections.Generic;

namespace Command.Test
{
    public class Tests
    {
        private IProductList _ProductList;
        private IProduct _Product1;
        private IProduct _Product2;

        [SetUp]
        public void Setup()
        {
            _ProductList = Mock.Of<IProductList>
            (
                productList => 
                    productList.Name == "WishList" && 
                    productList.Products == (new List<IProduct>())
            );

            _Product1 = Mock.Of<IProduct>
            (
                product =>
                    product.Name == "Product 1" &&
                    product.Price == 45.00M
            );
            
            _Product2 = Mock.Of<IProduct>
            (
                product =>
                    product.Name == "Product 2" &&
                    product.Price == 55.00M
            );
            
        }

        [Test]
        public void ItemsAdded_WhenUsingAddCommand()
        {
            // Arrange
            var addProd1Command = new AddCommand(_ProductList, _Product1);
            var addProd2Command = new AddCommand(_ProductList, _Product2);

            // Act
            addProd1Command.Execute();
            addProd2Command.Execute();

            // Assert
            _ProductList.Products.Should().NotBeNullOrEmpty()
                                          .And
                                          .HaveCount(2)
                                          .And
                                          .OnlyHaveUniqueItems()
                                          .And
                                          .Contain(_Product1)
                                          .And
                                          .Contain(_Product2);
        }

        [Test]
        public void ItemsRemoved_WhenUsingRemovingCommand()
        {
            // Arrange
            var addProd1Command = new AddCommand(_ProductList, _Product1);
            var addProd2Command = new AddCommand(_ProductList, _Product2);
            addProd1Command.Execute();
            addProd2Command.Execute();

            var removeProduct2Command = new RemoveCommand(_ProductList, _Product2);
            
            // Act
            removeProduct2Command.Execute();

            // Assert
            _ProductList.Products.Should().NotBeNullOrEmpty()
                                          .And
                                          .HaveCount(1)
                                          .And
                                          .OnlyHaveUniqueItems()
                                          .And
                                          .NotContain(_Product2)
                                          .And
                                          .Contain(_Product1);
        }

        
    }
}