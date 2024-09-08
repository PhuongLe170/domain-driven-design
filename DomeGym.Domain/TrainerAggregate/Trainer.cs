using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.TrainerAggregate;

public class Trainer : AggregateRoot
{
    private readonly List<Guid> _sessionIds = new();
    private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();

    public Trainer(
        Guid userId,
        Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
        _schedule = schedule ?? Schedule.Empty();
    }

    public ErrorOr<Success> AddSessionToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
        {
            return Error.Conflict("Session is already added");
        }

        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);

        if (bookTimeSlotResult.IsError && bookTimeSlotResult.FirstError.Type == ErrorType.Conflict)
        {
            return TrainerErrors.CannotHaveTwoOrMoreOverlappingSessions;
        }


        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}
