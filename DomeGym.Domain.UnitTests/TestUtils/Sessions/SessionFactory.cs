using Constants = DomeGym.Domain.UnitTests.TestConstants.Constants;

namespace DomeGym.Domain.UnitTests.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        DateOnly? date = null,
        TimeRange? time = null,
        int maxParticipants = Constants.Session.MaxParticipants,
        Guid? id = null)
        => new Session(
            date ?? Constants.Session.Date,
            time ?? Constants.Session.Time,
            maxParticipants: maxParticipants,
            trainerId: Constants.Trainer.Id,
            id: id ?? Guid.NewGuid());
}
