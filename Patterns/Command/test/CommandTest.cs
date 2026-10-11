using Command;
using System;
using System.Collections.Generic;
using Xunit;
using Shouldly;
using System.Linq;
using NSubstitute;


namespace CommandTest;

public class CommandShould
{
    private readonly IProductList _productList;
    private readonly IInvoker _productInvoker;

    public CommandShould()
    {
        _productList = Substitute.For<IProductList>();
        _productList.Name.Returns("WishList");
        _productList.Products.Returns([]);

        _productInvoker = new ProductCommandInvoker();
    }

    public static IEnumerable<object[]> GetProducts()
    {
        var product1 = Substitute.For<IProduct>();
        product1.Name.Returns("Product 1");
        product1.Price.Returns(45.00M);

        var product2 = Substitute.For<IProduct>();
        product2.Name.Returns("Product 2");
        product2.Price.Returns(55.00M);

        yield return new object[] {
            product1,
            product2
        };
    }

    [Theory]
    [MemberData(nameof(GetProducts))]
    public void AddItems_WhenUsingAddCommand(IProduct product1, IProduct product2)
    {
        // Arrange
        var addProduct1Command = new AddCommand(_productList, product1);
        var addProduct2Command = new AddCommand(_productList, product2);

        // Act
        _productInvoker.AddCommand("addproduct1", addProduct1Command);
        _productInvoker.AddCommand("addproduct2", addProduct2Command);
        _productInvoker.InvokeCommand("addproduct1");
        _productInvoker.InvokeCommand("addproduct2");

        // Assert
        var products = _productList.Products;

        products.ShouldSatisfyAllConditions(
            "product list",
            () => products.ShouldNotBeEmpty(),
            () => products.Distinct().Count().ShouldBe(2),
            () => products.ShouldContain(product1),
            () => products.ShouldContain(product2)
        );
    }

    [Theory]
    [MemberData(nameof(GetProducts))]
    public void AddTwoRemoveOneCorrectly_WhenUsingRemoveCommand(IProduct product1, IProduct product2)
    {
        // Arrange
        var addProduct1Command = new AddCommand(_productList, product1);
        _productInvoker.AddCommand("addproduct1", addProduct1Command);

        var addProduct2Command = new AddCommand(_productList, product2);
        _productInvoker.AddCommand("addproduct2", addProduct2Command);

        var removeProduct2Command = new RemoveCommand(_productList, product2);
        _productInvoker.AddCommand("removeproduct2", removeProduct2Command);

        // Act
        _productInvoker.InvokeCommand("addproduct1");
        _productInvoker.InvokeCommand("addproduct2");
        _productInvoker.InvokeCommand("removeproduct2");

        // Assert
        var products = _productList.Products;

        products.ShouldSatisfyAllConditions(
            "product list",
            () => products.ShouldNotBeEmpty(),
            () => products.ShouldHaveSingleItem(),
            () => products.ShouldNotContain(product2),
            () => products.ShouldContain(product1)
        );
    }

    [Theory]
    [MemberData(nameof(GetProducts))]
    public void ClearItems_WhenUsingClearCommand(IProduct product1, IProduct product2)
    {
        // Arrange
        var addProduct1Command = new AddCommand(_productList, product1);
        _productInvoker.AddCommand("addproduct1", addProduct1Command);

        var addProduct2Command = new AddCommand(_productList, product2);
        _productInvoker.AddCommand("addproduct2", addProduct2Command);

        var clearCommand = new ClearCommand(_productList);
        _productInvoker.AddCommand("clearall", clearCommand);

        // Act
        _productInvoker.InvokeCommand("addproduct1");
        _productInvoker.InvokeCommand("addproduct2");
        _productInvoker.InvokeCommand("clearall");

        // Assert
        _productList.Products.ShouldBeEmpty();
    }
}
