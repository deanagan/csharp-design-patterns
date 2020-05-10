using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using FluentAssertions;

namespace ProxyPattern.Tests
{
    public class Tests
    {
        Dictionary<int, IProductInfo> _products;
        IEntries _entries;
        IUser _adminUser;
        IUser _regularUser;
        delegate IAuthenticationController CreateAuthController(IUser user);
        CreateAuthController _createAuthController;
        [SetUp]
        public void Setup()
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

        [Test]
        public void GetProductInfoSuccess_WhenReadingAsRegularUser()
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

        [Test]
        public void GetProductInfoSuccess_WhenReadingAsAdminUser()
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

        [Test]
        public void DeleteProductInfoSuccess_WhenDeletingAsAdminUser()
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

        [Test]
        public void DeleteProductInfoFailed_WhenDeletingAsRegularUser()
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