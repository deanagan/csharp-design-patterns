using NUnit.Framework;
using Moq;
using FluentAssertions;
using Mediator;

namespace Mediator.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AlertScreen_WhenOnePurchaserCompletesTransaction()
        {
            // Arrange
            var alertScreenForCompletedPurchaser = new Mock<IAlertScreen>();
            var alertScreenForActivePurchaser = new Mock<IAlertScreen>();
            var mediator = new PurchaseMediator();
            var completedPurchaser = new Purchaser(alertScreenForCompletedPurchaser.Object, mediator);
            var activePurchaser = new Purchaser(alertScreenForActivePurchaser.Object, mediator);
            mediator.AddPurchaser(completedPurchaser);
            mediator.AddPurchaser(activePurchaser);

            // Act
            completedPurchaser.Complete();

            // Assert
            alertScreenForActivePurchaser.Verify(sc => sc.ShowMessage("Ball", "Sydney"));
            alertScreenForCompletedPurchaser.Verify(sc => sc.ShowMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }
    }
}