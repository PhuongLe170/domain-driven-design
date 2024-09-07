using ErrorOr;

namespace DomeGym.Domain;

public class Room
{
    private readonly List<Guid> _sessionIds = new();
    private readonly int _maxDailySessions;
    private readonly Guid _gymId;
    private readonly Schedule _schedule = Schedule.Empty();
    public Guid Id { get; }

    public Room(
        int maxDailySessions,
        Guid gymId,
        Schedule? schedule = null,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        _maxDailySessions = maxDailySessions;
        _gymId = gymId;
        schedule = schedule ?? Schedule.Empty();
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Contains(session.Id))
        {
            return Error.Conflict(description:"Session already exists in room");
        }

        if (_sessionIds.Count >= _maxDailySessions)
        {
            return RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows;
        }

        var addEventResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (addEventResult.IsError && addEventResult.FirstError.Type == ErrorType.Conflict)
        {
            return RoomErrors.CannotHaveTwoOrMoreOverlappingSessions;
        }

        _sessionIds.Add(session.Id);

        return Result.Success;
    }

}
