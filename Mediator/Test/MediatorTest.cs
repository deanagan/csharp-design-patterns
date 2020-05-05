using System;
using Xunit;
using Moq;
using FluentAssertions;

namespace Mediator.Test
{
    public class MediatorShould
    {
        private Product _product;
        private IMediator _mediator;

        public MediatorShould()
        {
            _product = new Product { Item = "Ball", Location = "Sydney" };
            _mediator = new PurchaseMediator();
        }

        [Fact]
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
            using (new FluentAssertions.Execution.AssertionScope("purchaser"))
            {
                alertScreenForActivePurchaser.Verify(sc => sc.ShowMessage(_product.Item, _product.Location));
                alertScreenForCompletedPurchaser.Verify(sc => sc.ShowMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            }
        }

        [Fact]
        public void ExpectExceptionThrownByPurchaser_WhenAlertScreenIsNull()
        {
            // Act
            Action act = () => new Purchaser(null, _mediator);
            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ExpectExceptionThrownByPurchaser_WhenMediatorIsNull()
        {
            // Arrange
            var alertScreen = Mock.Of<IAlertScreen>();
            // Act
            Action act = () => new Purchaser(alertScreen, null);
            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ReturnFalse_WhenPurchaserIsNotRegistered()
        {
            // Arrange
            var purchaser = Mock.Of<IPurchaser>();
            // Act
            var result = _mediator.BroadcastPurchaseCompletion(purchaser);
            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalse_WhenPurchaserRegisteredTwice()
        {
            // Arrange
            var purchaser = Mock.Of<IPurchaser>();
            // Act
            var result = _mediator.AddPurchaser(purchaser) && _mediator.AddPurchaser(purchaser);
            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void NotCallAlertScreen_WhenThereIsNoPurchaserToAlert()
        {
            // Arrange
            var alertScreen = Mock.Of<IAlertScreen>();
            var purchaser = new Purchaser(alertScreen, _mediator);
            // Act
            _mediator.AddPurchaser(purchaser);
            purchaser.Complete(_product);
            // Assert
            Mock.Get(alertScreen).Verify(asc => asc.ShowMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
