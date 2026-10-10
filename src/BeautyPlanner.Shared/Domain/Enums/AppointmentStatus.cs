namespace BeautyPlanner.Shared.Domain.Enums;

public enum AppointmentStatus
{
    Created,        // Just created, not yet confirmed
    Confirmed,      // Confirmed by client or system
    InProgress,     // Staff has started the appointment
    Completed,      // Finished successfully
    CanceledByClient,
    CanceledByStaff,
    NoShow,         // Client did not show up
    Expired         // Time passed but no completion/cancel
}
