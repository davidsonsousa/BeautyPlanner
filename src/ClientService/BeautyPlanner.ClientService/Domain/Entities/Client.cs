namespace BeautyPlanner.ClientService.Domain.Entities;

public class Client : Person
{
    public Client(string firstName, string lastName, string email, string phoneNumber, DateTime? dateOfBirth, Address? address, int tenantId)
    {
        SetPropertyValues(firstName, lastName, email, phoneNumber, dateOfBirth, address);

        TenantId = tenantId;
    }

    // Navigation properties
    public int TenantId { get; set; }


    public void Update(string firstName, string lastName, string email, string phoneNumber, DateTime? dateOfBirth, Address? address)
    {
        SetPropertyValues(firstName, lastName, email, phoneNumber, dateOfBirth, address);
    }
}
