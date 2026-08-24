namespace BeautyPlanner.StaffService.Domain.Entities;

public class StaffMember : Person
{
    public StaffMember(string firstName, string lastName, string email, string phoneNumber, int professionId, DateTime? dateOfBirth, Address? address, Guid salonVanityId)
    {
        SetPropertyValues(firstName, lastName, email, phoneNumber, dateOfBirth, address);

        ProfessionId = professionId;
        SalonVanityId = salonVanityId;
    }

    // Navigation properties
    public Guid SalonVanityId { get; set; }

    public int ProfessionId { get; set; }

    public virtual Profession Profession { get; set; } = null!;

    public virtual ICollection<AvailabilityPeriod> AvailabilityPeriods { get; set; } = null!;


    public void Update(string firstName, string lastName, string email, string phoneNumber, int professionId, DateTime? dateOfBirth, Address? address)
    {
        SetPropertyValues(firstName, lastName, email, phoneNumber, dateOfBirth, address);

        ProfessionId = professionId;
    }
}
