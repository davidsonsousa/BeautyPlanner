namespace BeautyPlanner.StaffService.UnitTests.Domain.Entities;

public class AvailabilityPeriodTests
{
    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentException_When_StartTimeIsAfterEndTime()
    {
        Assert.Throws<ArgumentException>(() => new AvailabilityPeriodBuilder().WithStartTime(TimeSpan.FromHours(12))
                                                                              .WithEndTime(TimeSpan.FromHours(9))
                                                                              .Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentException_When_StartTimeEqualsEndTime()
    {
        Assert.Throws<ArgumentException>(() => new AvailabilityPeriodBuilder().WithStartTime(TimeSpan.FromHours(12))
                                                                              .WithEndTime(TimeSpan.FromHours(12))
                                                                              .Build());
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentException_When_StartTimeIsAfterEndTime()
    {
        var availabilityPeriod = new AvailabilityPeriodBuilder().Build();

        Assert.Throws<ArgumentException>(() => availabilityPeriod.Update(DayOfWeek.Tuesday, TimeSpan.FromHours(12), TimeSpan.FromHours(9)));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentException_When_StartTimeEqualsEndTime()
    {
        var availabilityPeriod = new AvailabilityPeriodBuilder().Build();

        Assert.Throws<ArgumentException>(() => availabilityPeriod.Update(DayOfWeek.Tuesday, TimeSpan.FromHours(12), TimeSpan.FromHours(12)));
    }
}
