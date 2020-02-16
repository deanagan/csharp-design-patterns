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
        private IProduct _CleanCodeBook;
        private IProduct _RefactoringBook;

        [SetUp]
        public void Setup()
        {
            _ProductList = Mock.Of<IProductList>
            (
                productList => 
                    productList.Name == "WishList" && 
                    productList.Products == (new List<IProduct>())
            );

            _CleanCodeBook = Mock.Of<IProduct>
            (
                product =>
                    product.Name == "Clean Code" &&
                    product.Price == 45.00M
            );
            
            _RefactoringBook = Mock.Of<IProduct>
            (
                product =>
                    product.Name == "Refactoring" &&
                    product.Price == 55.00M
            );
            
        }

        [Test]
        public void ItemsAdded_WhenUsingAddCommand()
        {
            // Arrange
            var addCommand = new AddCommand(_ProductList);

            // Act
            addCommand.Execute(_CleanCodeBook);
            addCommand.Execute(_RefactoringBook);

            // Assert
            _ProductList.Products.Should().NotBeNullOrEmpty()
                                          .And
                                          .HaveCount(2)
                                          .And
                                          .OnlyHaveUniqueItems()
                                          .And
                                          .Contain(_CleanCodeBook)
                                          .And
                                          .Contain(_RefactoringBook);
        }

        [Test]
        public void ItemsRemoved_WhenUsingRemovingCommand()
        {
            // Arrange
            var addCommand = new AddCommand(_ProductList);
            addCommand.Execute(_CleanCodeBook);
            addCommand.Execute(_RefactoringBook);

            var removeCommand = new RemoveCommand(_ProductList);
            
            // Act
            removeCommand.Execute(_CleanCodeBook);

            // Assert
            _ProductList.Products.Should().NotBeNullOrEmpty()
                                          .And
                                          .HaveCount(1)
                                          .And
                                          .OnlyHaveUniqueItems()
                                          .And
                                          .NotContain(_CleanCodeBook)
                                          .And
                                          .Contain(_RefactoringBook);
        }
    }
}