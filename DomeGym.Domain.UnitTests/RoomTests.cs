using DomeGym.Domain.UnitTests.TestUtils.Common;
using DomeGym.Domain.UnitTests.TestUtils.Rooms;
using DomeGym.Domain.UnitTests.TestUtils.Sessions;
using FluentAssertions;
using Constants = DomeGym.Domain.UnitTests.TestConstants.Constants;

namespace DomeGym.Domain.UnitTests;

public class RoomTests
{
    [Fact]
    public void ScheduleSession_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        var room = RoomFactory.CreateRoom(id: Guid.NewGuid(), maxDailySessions:1);
        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid());

        // Act
        var scheduleSession1Result = room.ScheduleSession(session1);
        var scheduleSession2Result = room.ScheduleSession(session2);

        // Assert
        scheduleSession1Result.IsError.Should().BeFalse();

        scheduleSession2Result.IsError.Should().BeTrue();

        scheduleSession2Result.FirstError.Should().Be(RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows);
    }

    [Theory]
    [InlineData(1,3,1,3)] //exact overlap
    [InlineData(1, 4, 2,4)]
    [InlineData(1, 3, 2,4)]
    [InlineData(1, 3, 0,2)]
    public void ScheduleSession_WhenSessionOverlapWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        var room = RoomFactory.CreateRoom(id: Guid.NewGuid(), maxDailySessions:2);

        var session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        var session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        var scheduleSession1Result = room.ScheduleSession(session1);
        var scheduleSession2Result = room.ScheduleSession(session2);

        // Assert
        scheduleSession1Result.IsError.Should().BeFalse();

        scheduleSession2Result.IsError.Should().BeTrue();
        scheduleSession2Result.FirstError.Should().Be(RoomErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}
