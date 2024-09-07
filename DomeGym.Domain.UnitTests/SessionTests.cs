using DomeGym.Domain.UnitTests.TestUtils.Participants;
using DomeGym.Domain.UnitTests.TestUtils.Services;
using DomeGym.Domain.UnitTests.TestUtils.Sessions;
using FluentAssertions;
using Constants = DomeGym.Domain.UnitTests.TestConstants.Constants;

namespace DomeGym.Domain.UnitTests;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maxParticipants: 1);
        var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());

        // Act
        var reserveParticipant1Result =  session.ReserveSpot(participant1);
        var reserveParticipant2Result =  session.ReserveSpot(participant2);

        // Assert
        reserveParticipant1Result.IsError.Should().BeFalse();

        reserveParticipant2Result.IsError.Should().BeTrue();

        reserveParticipant2Result.FirstError.Should().Be(SessionErrors.CannotHaveMoreReservationsThanParticipants);
    }

    [Fact]
    public void CancelReservation_WhenCancellationIsTooCloseToSession_ShouldFailCancellation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: Constants.Session.Time
        );

        var participant = ParticipantFactory.CreateParticipant();
        var reserveParticipantResult =  session.ReserveSpot(participant);

        var cancellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);
        // Act

        var cancelReservationResult = session.CancelReservation(
            participant,
            new TestDateTimeProvider(cancellationDateTime));

        // Assert
        reserveParticipantResult.IsError.Should().BeFalse();

        cancelReservationResult.IsError.Should().BeTrue();
        cancelReservationResult.FirstError.Should().Be(SessionErrors.CannotCancelReservationTooCloseToSession);
    }
}
