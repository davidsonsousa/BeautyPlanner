namespace BeautyPlanner.TenantService.Domain.Entities;

public class Salon : AuditableEntity
{
    public Salon()
    {
        
    }

    public Salon(string name, string? description, string email, string phoneNumber, Address address, int tenantId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        
        ArgumentNullException.ThrowIfNull(address);
        Address = new Address(address.Line1, address.Line2, address.PostalCode, address.City, address.StateProvince, address.Country);
        
        TenantId = tenantId;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public string Email { get; private set; }
    
    public string PhoneNumber { get; private set; }

    public Address Address { get; private set; }


    // Navigation properties
    public int TenantId { get; private set; }

    public virtual Tenant Tenant { get; private set; } = null!;

    public void Update(string name, string? description, string email, string phoneNumber, Address address)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));

        ArgumentNullException.ThrowIfNull(address);
        Address = new Address(address.Line1, address.Line2, address.PostalCode, address.City, address.StateProvince, address.Country);
    }
}
