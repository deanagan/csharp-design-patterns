using System;
using NSubstitute;
using Observer;
using Shouldly;
using Xunit;

namespace ObserverTest;

public class ObserverTest
{
    [Fact]
    public void NotifiesEveryAttachedObserverWhenApplicationStatusChanges()
    {
        var application = CreateApplication();
        var firstObserver = Substitute.For<ILoanApplicationObserver>();
        var secondObserver = Substitute.For<ILoanApplicationObserver>();
        application.Attach(firstObserver);
        application.Attach(secondObserver);

        application.ChangeStatus(LoanApplicationStatus.Submitted).ShouldBeTrue();

        var expectedChange = new LoanApplicationStatusChanged(
            "LN-1001",
            "Jordan Lee",
            LoanApplicationStatus.Draft,
            LoanApplicationStatus.Submitted);
        firstObserver.Received(1).OnStatusChanged(expectedChange);
        secondObserver.Received(1).OnStatusChanged(expectedChange);
        application.Status.ShouldBe(LoanApplicationStatus.Submitted);
    }

    [Fact]
    public void DoesNotNotifyObserversWhenStatusDoesNotChange()
    {
        var application = CreateApplication();
        var observer = Substitute.For<ILoanApplicationObserver>();
        application.Attach(observer);

        application.ChangeStatus(LoanApplicationStatus.Draft).ShouldBeFalse();

        observer.DidNotReceive().OnStatusChanged(Arg.Any<LoanApplicationStatusChanged>());
    }

    [Fact]
    public void StopsNotifyingAnObserverAfterItIsDetached()
    {
        var application = CreateApplication();
        var observer = Substitute.For<ILoanApplicationObserver>();
        application.Attach(observer);
        application.Detach(observer);

        application.ChangeStatus(LoanApplicationStatus.Submitted);

        observer.DidNotReceive().OnStatusChanged(Arg.Any<LoanApplicationStatusChanged>());
    }

    [Fact]
    public void NotifiesAnObserverOnlyOnceWhenAttachedMoreThanOnce()
    {
        var application = CreateApplication();
        var observer = Substitute.For<ILoanApplicationObserver>();
        application.Attach(observer);
        application.Attach(observer);

        application.ChangeStatus(LoanApplicationStatus.Submitted);

        observer.Received(1).OnStatusChanged(Arg.Any<LoanApplicationStatusChanged>());
    }

    [Fact]
    public void BorrowerObserverForwardsStatusChangeToNotificationService()
    {
        var notificationService = Substitute.For<IBorrowerStatusNotificationService>();
        var observer = new BorrowerNotificationObserver(notificationService);
        var change = new LoanApplicationStatusChanged(
            "LN-1001",
            "Jordan Lee",
            LoanApplicationStatus.UnderReview,
            LoanApplicationStatus.Approved);

        observer.OnStatusChanged(change);

        notificationService.Received(1).NotifyStatusChanged(
            "LN-1001",
            "Jordan Lee",
            LoanApplicationStatus.UnderReview,
            LoanApplicationStatus.Approved);
    }

    [Fact]
    public void RejectsNullObserverAndNotificationService()
    {
        var application = CreateApplication();

        Should.Throw<ArgumentNullException>(() => application.Attach(null!));
        Should.Throw<ArgumentNullException>(() => application.Detach(null!));
        Should.Throw<ArgumentNullException>(() => new BorrowerNotificationObserver(null!));
    }

    private static LoanApplication CreateApplication() =>
        new("LN-1001", "Jordan Lee");
}
