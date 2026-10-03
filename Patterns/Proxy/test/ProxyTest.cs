using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using FluentAssertions;

namespace Proxy.Test
{
    public class ProxyShould
    {
        Dictionary<int, IProductInfo> _products;
        IEntries _entries;
        IUser _adminUser;
        IUser _regularUser;
        delegate IAuthenticationController CreateAuthController(IUser user);
        CreateAuthController _createAuthController;

        public ProxyShould()
        {
            _products = new Dictionary<int, IProductInfo>
            {
                {1, Mock.Of<IProductInfo>(
                    productInfo =>
                        productInfo.Name == "Xbox360" &&
                        productInfo.Id == 1)
                }
            };
            _adminUser = Mock.Of<IUser> (user => user.IsAdmin == true);
            _regularUser = Mock.Of<IUser> (user => user.IsAdmin == false);

            _createAuthController = (user) => new AuthenticationController(user);
            _entries = new Entries(_products);
        }

        [Fact]
        public void GetProductInfoSuccessfully_WhenReadingAsRegularUser()
        {
            // Arrange
            var authCtrl = _createAuthController(_regularUser);
            var entries = new EntriesProxy(authCtrl, _entries);

            // Act
            var productInfo = entries.Get(1);

            //Assert
            productInfo.Id.Should().Be(1);
            productInfo.Name.Should().Be("Xbox360");
        }

        [Fact]
        public void GetProductInfoSuccessfully_WhenReadingAsAdminUser()
        {
            // Arrange
            var authCtrl = _createAuthController(_adminUser);
            var entries = new EntriesProxy(authCtrl, _entries);

            // Act
            var productInfo = entries.Get(1);

            //Assert
            productInfo.Id.Should().Be(1);
            productInfo.Name.Should().Be("Xbox360");
        }

        [Fact]
        public void DeleteProductInfoSuccessfully_WhenDeletingAsAdminUser()
        {
            // Arrange
            var authCtrl = _createAuthController(_adminUser);
            var entries = new EntriesProxy(authCtrl, _entries);

            // Act
            var isDeleted = entries.Delete(1);

            //Assert
            isDeleted.Should().Be(true);
            entries.Get(1).Should().Be(null);
        }

        [Fact]
        public void BeUnableToDeleteProductInfo_WhenDeletingAsRegularUser()
        {
            // Arrange
            var authCtrl = _createAuthController(_regularUser);
            var entries = new EntriesProxy(authCtrl, _entries);

            // Act
            var isDeleted = entries.Delete(1);

            //Assert
            isDeleted.Should().Be(false);
            entries.Get(1).Should().NotBe(null);
        }
    }
}
