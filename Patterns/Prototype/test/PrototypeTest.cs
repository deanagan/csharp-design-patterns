using Xunit;
using FluentAssertions;

namespace Prototype.Test
{
    public class PrototypeShould
    {
        private BasicCustomer _basicCustomer;

        public PrototypeShould()
        {
            _basicCustomer = new Customer();
            _basicCustomer.FirstName = "John";
            _basicCustomer.LastName = "Smith";
            _basicCustomer.HomeAddress = new Address
            {
                StreetAddress = "1 Kent Street",
                City = "Sydney",
                State = "New South Wales",
                Country = "Australia",
                PostCode = "2000"
            };
            _basicCustomer.BillingAddress = new Address
            {
                StreetAddress = _basicCustomer.HomeAddress.StreetAddress,
                City = _basicCustomer.HomeAddress.City,
                State = _basicCustomer.HomeAddress.State,
                Country = _basicCustomer.HomeAddress.Country,
                PostCode = _basicCustomer.HomeAddress.PostCode
            };
        }

        [Fact]
        public void ShallowCloneCustomerWithSameAddress_WhenAddressObjectsAreShared()
        {
            // Arrange
            var customer = (Customer)_basicCustomer.Clone();

            // Act
            var newStreetAddress = "26 York Street";
            var relatedCustomer = (Customer)customer.Clone();
            relatedCustomer.FirstName = "Jane";
            relatedCustomer.HomeAddress!.StreetAddress = newStreetAddress;

            // Assert
            customer.HomeAddress.Should().Be(relatedCustomer.HomeAddress);
        }

        [Fact]
        public void DeepCloneCustomerWithDifferentAddressButWithSameFirstName_WhenAddressObjectsAreDifferent()
        {
            // Arrange
            var customer = (Customer)_basicCustomer.Clone();

            // Act
            var diffCustomer = customer.DeepClone();
            diffCustomer.FirstName = "Peter";
            diffCustomer.LastName = "Wick";
            diffCustomer.HomeAddress!.StreetAddress = "57 Hassall Ridge";
            diffCustomer.HomeAddress.City = "Lexington";
            diffCustomer.HomeAddress.State = "Kentucky";
            diffCustomer.HomeAddress.Country = "United States Of America";
            diffCustomer.HomeAddress.PostCode = "40511";

            // Assert
            customer.HomeAddress.Should().NotBeEquivalentTo(diffCustomer.HomeAddress);
            customer.FirstName.Should().BeEquivalentTo("John");
        }
    }
}
