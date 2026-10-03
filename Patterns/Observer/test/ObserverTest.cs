using System.Collections.Generic;
using Xunit;
using Moq;
using FluentAssertions;

namespace Observer.Test
{
    public class ObserverShould
    {
        [Fact]
        public void UpdateObserverOnce_WhenSubjectHasSpecials()
        {
            // Arrange
            var subject = new SpecialsSubject
            {
                SubjectState = "Footwear Sale"
            };
            var mockObserver = new Mock<IObserver>();
            // Act
            subject.Attach(mockObserver.Object);
            subject.Notify();
            // Assert
            mockObserver.Verify(observer => observer.Update(subject), Times.Once());
        }

        [Fact]
        public void NotCallUpdate_WhenObserverNotAttachedToSubject()
        {
            // Arrange
            var subject = new SpecialsSubject
            {
                SubjectState = "Footwear Sale"
            };
            var mockObserver = new Mock<IObserver>();
            // Act
            subject.SubjectState = "Footwear Sale";
            subject.Notify();
            // Assert
            mockObserver.Verify(observer => observer.Update(subject), Times.Never());
        }

        [Fact]
        public void EchoMessage_WhenReceivingSubjectNotification()
        {
            // Arrange
            var mockSubject = new Mock<ISubject>();
            var customer = new Customer();
            // Act
            mockSubject.SetupGet(subj => subj.SubjectState).Returns("Footwear Sale");
            mockSubject.Object.Attach(customer);
            // Assert
            customer.Update(mockSubject.Object).Should().Be("Customer received Footwear Sale");
        }
    }
}
