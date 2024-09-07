using Constants = DomeGym.Domain.UnitTests.TestConstants.Constants;

namespace DomeGym.Domain.UnitTests.TestUtils.Participants;

public static class ParticipantFactory
{
    public static Participant CreateParticipant(
        Guid? id = null,
        Guid? userId = null)
        => new Participant(
            userId: userId ?? Constants.User.Id,
            id: id ?? Constants.Participant.Id);
}
