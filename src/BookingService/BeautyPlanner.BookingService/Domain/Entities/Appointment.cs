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

    public void Start()
    {
        if (Status != AppointmentStatus.Confirmed)
        {
            throw new InvalidOperationException("Only confirmed appointments can be started.");
        }

        Status = AppointmentStatus.InProgress;
    }

    public void Complete()
    {
        if (Status != AppointmentStatus.InProgress)
        {
            throw new InvalidOperationException("Only appointments in progress can be completed.");
        }

        Status = AppointmentStatus.Completed;
    }

    public void ClientCancel()
    {
        if (Status != AppointmentStatus.Created && Status != AppointmentStatus.Confirmed)
        {
            throw new InvalidOperationException("Only created or confirmed appointments can be canceled.");
        }

        Status = AppointmentStatus.CanceledByClient;
    }

    public void StaffCancel()
    {
        if (Status != AppointmentStatus.Created && Status != AppointmentStatus.Confirmed)
        {
            throw new InvalidOperationException("Only created or confirmed appointments can be canceled.");
        }

        Status = AppointmentStatus.CanceledByStaff;
    }

    public void MarkNoShow()
    {
        if (Status != AppointmentStatus.Confirmed)
        {
            throw new InvalidOperationException("Only confirmed appointments can be marked as no-show.");
        }

        Status = AppointmentStatus.NoShow;
    }

    public void Expire()
    {
        if (Status != AppointmentStatus.Created && Status != AppointmentStatus.Confirmed)
        {
            throw new InvalidOperationException("Only created or confirmed appointments can expire.");
        }

        Status = AppointmentStatus.Expired;
    }

    public void Update(TimeRange period, Guid clientId, Guid staffMemberId, Guid treatmentId)
    {
        if (Status == AppointmentStatus.Completed || Status == AppointmentStatus.InProgress)
        {
            throw new InvalidOperationException("Completed or in-progress appointments cannot be modified.");
        }

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