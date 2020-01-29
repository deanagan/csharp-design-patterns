using NUnit.Framework;
using Moq;
using FluentAssertions;
using Mediator;

namespace Mediator.Tests
{
    public class Tests
    {
        private Product _product;
        private IMediator _mediator;
        [SetUp]
        public void Setup()
        {
            _product = new Product { Item = "Ball", Location = "Sydney" };
            _mediator = new PurchaseMediator();
        }

        [Test]
        public void AlertScreen_WhenOnePurchaserCompletesTransaction()
        {
            // Arrange

            var alertScreenForCompletedPurchaser = new Mock<IAlertScreen>();
            var alertScreenForActivePurchaser = new Mock<IAlertScreen>();
            var completedPurchaser = new Purchaser(alertScreenForCompletedPurchaser.Object, _mediator);
            var activePurchaser = new Purchaser(alertScreenForActivePurchaser.Object, _mediator);
            _mediator.AddPurchaser(completedPurchaser);
            _mediator.AddPurchaser(activePurchaser);

            // Act
            completedPurchaser.Complete(_product);

            // Assert
            alertScreenForActivePurchaser.Verify(sc => sc.ShowMessage(_product.Item, _product.Location));
            alertScreenForCompletedPurchaser.Verify(sc => sc.ShowMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }
    }
}