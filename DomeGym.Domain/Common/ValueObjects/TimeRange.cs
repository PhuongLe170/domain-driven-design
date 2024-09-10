using ErrorOr;
using Throw;

namespace DomeGym.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public static ErrorOr<TimeRange> FormDateTimes(DateTime startDate, DateTime endDate)
    {
        if (startDate.Date != endDate.Date)
        {
            return Error.Validation(description: "Start date and end date must be on the same day.");
        }

        if (startDate >= endDate)
        {
            return Error.Validation(description: "Start date cannot be before end date.");
        }

        return new TimeRange(
            start: TimeOnly.FromDateTime(startDate),
            end: TimeOnly.FromDateTime(endDate));
    }

    public bool OverlapsWith(TimeRange other)
    {
        if (Start >= other.End)
            return false;
        return other.Start < End;
    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
