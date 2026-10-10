namespace BeautyPlanner.BookingService.Domain.Entities;

public class Appointment : AuditableEntity
{
    public Appointment()
    {

    }

    public Appointment(TimeRange timeRange, Guid clientId, Guid staffMemberId, Guid treatmentId, Guid salonId)
    {
        SetPropertyValues(timeRange);

        ClientId = clientId;
        StaffMemberId = staffMemberId;
        TreatmentId = treatmentId;
        SalonId = salonId;
        Status = AppointmentStatus.Created;
    }

    public TimeRange Period { get; private set; }

    public AppointmentStatus Status { get; private set; }

    // Navigation properties
    public Guid ClientId { get; private set; }

    public Guid StaffMemberId { get; private set; }

    public Guid TreatmentId { get; private set; }

    public Guid SalonId { get; private set; }

    public void Confirm()
    {
        if (Status != AppointmentStatus.Created)
        {
            throw new InvalidOperationException("Only created appointments can be confirmed.");
        }

        Status = AppointmentStatus.Confirmed;
    }

    public void ClientCancel()
    {
        if (Status == AppointmentStatus.Completed)
        {
            throw new InvalidOperationException("Completed appointments cannot be canceled.");
        }

        Status = AppointmentStatus.CanceledByClient;
    }

    public void StaffCancel()
    {
        if (Status == AppointmentStatus.Completed)
        {
            throw new InvalidOperationException("Completed appointments cannot be canceled.");
        }

        Status = AppointmentStatus.CanceledByStaff;
    }

    public void Complete()
    {
        if (Status != AppointmentStatus.Confirmed)
        {
            throw new InvalidOperationException("Only confirmed appontments can be completed.");
        }

        Status = AppointmentStatus.Completed;
    }

    public void Update(TimeRange period, Guid clientId, Guid staffMemberId, Guid treatmentId)
    {
        SetPropertyValues(period);

        ClientId = clientId;
        StaffMemberId = staffMemberId;
        TreatmentId = treatmentId;
    }

    private void SetPropertyValues(TimeRange period)
    {
        Period = period;
    }
}
