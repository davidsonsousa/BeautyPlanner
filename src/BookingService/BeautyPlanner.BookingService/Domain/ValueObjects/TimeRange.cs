namespace BeautyPlanner.BookingService.Domain.ValueObjects;

public class TimeRange
{
    public TimeRange()
    {

    }

    public TimeRange(DateTime startTime, DateTime endTime)
    {
        if (startTime >= endTime)
        {
            throw new ArgumentException("Start time must be before end time.");
        }

        StartTime = startTime;
        EndTime = endTime;
    }

    public DateTime StartTime { get; init; }

    public DateTime EndTime { get; init;  }

    public TimeSpan Duration
    {
        get
        {
            return EndTime - StartTime;
        }
    }
}
