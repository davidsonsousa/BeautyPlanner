namespace BeautyPlanner.BookingService.UnitTests.Domain.Entities;

public class AppointmentTests
{
    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_SetProperties_When_ValidValuesAreProvided()
    {
        var period = new TimeRange(new DateTime(2026, 11, 15, 10, 0, 0), new DateTime(2026, 11, 15, 10, 30, 0));

        var appointment = new AppointmentBuilder().WithTimeRange(period)
                                                  .Build();

        Assert.Equal(period, appointment.Period);
        Assert.Equal(AppointmentStatus.Created, appointment.Status);
    }

    [Trait("Category", "Confirm")]
    [Fact]
    public void Confirm_Should_ChangeStatus_When_StatusIsCreated()
    {
        var appointment = new AppointmentBuilder().Build();

        appointment.Confirm();

        Assert.Equal(AppointmentStatus.Confirmed, appointment.Status);
    }

    [Trait("Category", "Confirm")]
    [Fact]
    public void Confirm_Should_ThrowInvalidOperationException_When_StatusIsNotCreated()
    {
        var appointment = CreateInProgressAppointment();

        Assert.Throws<InvalidOperationException>(() => appointment.Confirm());
    }

    [Trait("Category", "Start")]
    [Fact]
    public void Start_Should_ChangeStatus_When_StatusIsConfirmed()
    {
        var appointment = CreateConfirmedAppointment();

        appointment.Start();

        Assert.Equal(AppointmentStatus.InProgress, appointment.Status);
    }

    [Trait("Category", "Start")]
    [Fact]
    public void Start_Should_ThrowInvalidOperationException_When_StatusIsNotConfirmed()
    {
        var appointment = new AppointmentBuilder().Build();

        Assert.Throws<InvalidOperationException>(() => appointment.Start());
    }

    [Trait("Category", "Complete")]
    [Fact]
    public void Complete_Should_ChangeStatus_When_StatusIsInProgress()
    {
        var appointment = CreateInProgressAppointment();

        appointment.Complete();

        Assert.Equal(AppointmentStatus.Completed, appointment.Status);
    }

    [Trait("Category", "Complete")]
    [Fact]
    public void Complete_Should_ThrowInvalidOperationException_When_StatusIsNotInProgress()
    {
        var appointment = CreateConfirmedAppointment();

        Assert.Throws<InvalidOperationException>(() => appointment.Complete());
    }

    [Trait("Category", "ClientCancel")]
    [Fact]
    public void ClientCancel_Should_ChangeStatus_When_StatusIsCreated()
    {
        var appointment = new AppointmentBuilder().Build();

        appointment.ClientCancel();

        Assert.Equal(AppointmentStatus.CanceledByClient, appointment.Status);
    }

    [Trait("Category", "ClientCancel")]
    [Fact]
    public void ClientCancel_Should_ChangeStatus_When_StatusIsConfirmed()
    {
        var appointment = CreateConfirmedAppointment();

        appointment.ClientCancel();

        Assert.Equal(AppointmentStatus.CanceledByClient, appointment.Status);
    }

    [Trait("Category", "ClientCancel")]
    [Fact]
    public void ClientCancel_Should_ThrowInvalidOperationException_When_StatusIsInProgress()
    {
        var appointment = CreateInProgressAppointment();

        Assert.Throws<InvalidOperationException>(() => appointment.ClientCancel());
    }

    [Trait("Category", "StaffCancel")]
    [Fact]
    public void StaffCancel_Should_ChangeStatus_When_StatusIsCreated()
    {
        var appointment = new AppointmentBuilder().Build();

        appointment.StaffCancel();

        Assert.Equal(AppointmentStatus.CanceledByStaff, appointment.Status);
    }

    [Trait("Category", "StaffCancel")]
    [Fact]
    public void StaffCancel_Should_ChangeStatus_When_StatusIsConfirmed()
    {
        var appointment = CreateConfirmedAppointment();

        appointment.StaffCancel();

        Assert.Equal(AppointmentStatus.CanceledByStaff, appointment.Status);
    }

    [Trait("Category", "StaffCancel")]
    [Fact]
    public void StaffCancel_Should_ThrowInvalidOperationException_When_StatusIsInProgress()
    {
        var appointment = CreateInProgressAppointment();

        Assert.Throws<InvalidOperationException>(() => appointment.StaffCancel());
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_UpdateProperties_When_ValidValuesAreProvided()
    {
        var originalPeriod = new TimeRange(new DateTime(2026, 11, 15, 10, 0, 0), new DateTime(2026, 11, 15, 10, 30, 0));

        var newPeriod = new TimeRange(new DateTime(2026, 11, 16, 14, 0, 0), new DateTime(2026, 11, 16, 15, 0, 0));

        var appointment = new AppointmentBuilder().WithTimeRange(originalPeriod)
                                                  .Build();

        appointment.Update(newPeriod, appointment.ClientId, appointment.StaffMemberId, appointment.TreatmentId);

        Assert.Equal(newPeriod, appointment.Period);
        Assert.Equal(AppointmentStatus.Created, appointment.Status);
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_PreserveStatus_When_AppointmentIsConfirmed()
    {
        var appointment = CreateConfirmedAppointment();

        var newPeriod = new TimeRange(new DateTime(2026, 11, 16, 14, 0, 0), new DateTime(2026, 11, 16, 15, 0, 0));

        appointment.Update(newPeriod, appointment.ClientId, appointment.StaffMemberId, appointment.TreatmentId);

        Assert.Equal(AppointmentStatus.Confirmed, appointment.Status);
    }

    private static Appointment CreateConfirmedAppointment()
    {
        var appointment = new AppointmentBuilder().Build();

        appointment.Confirm();

        return appointment;
    }

    private static Appointment CreateInProgressAppointment()
    {
        var appointment = CreateConfirmedAppointment();

        appointment.Start();

        return appointment;
    }
}