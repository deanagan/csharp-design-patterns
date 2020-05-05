using System;
using NUnit.Framework;
using Moq;
using FluentAssertions;

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

            // Act
            _mediator.AddPurchaser(completedPurchaser);
            _mediator.AddPurchaser(activePurchaser);
            completedPurchaser.Complete(_product);

            // Assert
            alertScreenForActivePurchaser.Verify(sc => sc.ShowMessage(_product.Item, _product.Location));
            alertScreenForCompletedPurchaser.Verify(sc => sc.ShowMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void PurchaserThrowsException_WhenAlertScreenIsNull()
        {
            // Act
            Action act = () => new Purchaser(null, _mediator);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void PurchaserThrowsException_WhenMediatorIsNull()
        {
            // Arrange
            var alertScreen = new Mock<IAlertScreen>();

            // Act
            Action act = () => new Purchaser(alertScreen.Object, null);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void MediatorReturnsFalse_WhenPurchaserIsNotRegistered()
        {
            // Arrange
            var alertScreen = new Mock<IAlertScreen>();
            var purchaser = new Mock<IPurchaser>();

            // Act

            // Assert
            _mediator.BroadcastPurchaseCompletion(purchaser.Object).Should().Be(false);

        }

        [Test]
        public void MediatorReturnsFalse_WhenPurchaserRegisteredTwice()
        {
            // Arrange
            var purchaser = new Mock<IPurchaser>();

            // Act
            _mediator.AddPurchaser(purchaser.Object);

            // Assert
            _mediator.AddPurchaser(purchaser.Object).Should().Be(false);
        }

        [Test]
        public void AlertScreenNotCalled_WhenThereIsNoPurchaserToAlert()
        {
            // Arrange
            var alertScreen = new Mock<IAlertScreen>();
            var purchaser = new Purchaser(alertScreen.Object, _mediator);

            // Act
            _mediator.AddPurchaser(purchaser);
            purchaser.Complete(_product);

            // Assert
            alertScreen.Verify(asc => asc.ShowMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }
    }
}