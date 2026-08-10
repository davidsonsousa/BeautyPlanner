namespace BeautyPlanner.ClientService.Domain.Entities;

public abstract class Person : AuditableEntity
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public string Email { get; private set; }

    public string PhoneNumber { get; private set; }

    public DateTime? DateOfBirth { get; private set; }

    public Address? Address { get; private set; }

    protected void SetPropertyValues(string firstName, string lastName, string email, string phoneNumber, DateTime? dateOfBirth, Address? address)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        DateOfBirth = dateOfBirth;

        ArgumentNullException.ThrowIfNull(address);
        Address = new Address(address.Line1, address.Line2, address.PostalCode, address.City, address.StateProvince, address.Country);
    }
}
