namespace BeautyPlanner.ClientService.Domain.Entities;

public class Client : Person
{
    public Client()
    {

    }

    public Client(string firstName, string lastName, string email, string phoneNumber, DateTime? dateOfBirth, Address? address, Guid tenantId)
    {
        SetPropertyValues(firstName, lastName, email, phoneNumber, dateOfBirth, address);

        TenantVanityId = tenantId;
    }

    // Navigation properties
    public Guid TenantVanityId { get; set; }


    public void Update(string firstName, string lastName, string email, string phoneNumber, DateTime? dateOfBirth, Address? address)
    {
        SetPropertyValues(firstName, lastName, email, phoneNumber, dateOfBirth, address);
    }
}
