using Throw;

namespace DomeGym.Domain;

public class TimeRange
{
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public bool OverlapsWith(TimeRange other)
    {
        if (Start >= other.End)
            return false;
        return other.Start < End;
    }
}
