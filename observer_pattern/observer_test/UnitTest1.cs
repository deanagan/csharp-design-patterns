using System.Collections.Generic;
using NUnit.Framework;
using observer;
using Moq;

namespace observer_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SpecialSubjectHasSpecials_ObserverUpdated_MustCallUpdateOnce()
        {
            var subject = new SpecialsSubject();

            var mockObserver = new Mock<IObserver>();

            subject.Attach(mockObserver.Object);
            subject.SubjectState = "Footwear Sale";
            subject.Notify();
            mockObserver.Verify(observer => observer.Update(subject), Times.Once());
            subject.Detach(mockObserver.Object);

        }

        [Test]
        public void SpecialSubjectHasSpecials_ObserverDetach_MustNotCallUpdate()
        {
            var subject = new SpecialsSubject();

            var mockObserver = new Mock<IObserver>();
            subject.SubjectState = "Footwear Sale";
            subject.Notify();
            mockObserver.Verify(observer => observer.Update(subject), Times.Never());
        }

        [Test]
        public void CustomerReceivesSpecials_MustEchoCorrectMessage()
        {
            var mockSubject = new Mock<ISubject>();
            var mockObserver = new Mock<Customer>();
            mockSubject.SetupGet(subj => subj.SubjectState).Returns("Footwear Sale");


            mockSubject.Object.Attach(mockObserver.Object);
            mockSubject.Object.Notify();

            Assert.AreEqual("Customer received Footwear Sale", mockObserver.Object.Update(mockSubject.Object));
        }
    }
}