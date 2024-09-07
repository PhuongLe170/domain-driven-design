using ErrorOr;

namespace DomeGym.Domain;

public class Schedule
{
    private readonly Guid? _id;
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar = new();

    public Schedule(
        Dictionary<DateOnly, List<TimeRange>>? calendar = null,
        Guid? id = null)
    {
        _calendar = calendar ?? new();
        _id = id;
    }

    public static Schedule Empty() => new(id: Guid.NewGuid());

    internal bool CanBookTimeSlot(DateOnly date, TimeRange time)
    {
        if(!_calendar.TryGetValue(date, out var timeSlots))
        {
            return true;
        }

        return !timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time));
    }

    internal ErrorOr<Success> BookTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots))
        {
            _calendar[date] = new() { time };
            return Result.Success;
        }

        if (timeSlots.Any(timeslot => timeslot.OverlapsWith(time)))
        {
            return Error.Conflict();
        }

        timeSlots.Add(time);

        return Result.Success;
    }

    internal ErrorOr<Success> RemoveBooking(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots) || !timeSlots.Contains(time))
        {
            return Error.NotFound("Booking not found");
        }

        if (!timeSlots.Remove(time))
        {
            return Error.Unexpected();
        }

        return Result.Success;
    }

    private Schedule(){}
}
