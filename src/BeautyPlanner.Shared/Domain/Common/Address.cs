namespace BeautyPlanner.Shared.Domain.Common;

public class Address
{
    public Address()
    {
        
    }

    public Address(string line1, string? line2, string postalCode, string city, string? stateProvince, string country)
    {
        Line1 = line1 ?? throw new ArgumentNullException(nameof(line1));
        Line2 = line2;
        PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        City = city ?? throw new ArgumentNullException(nameof(city));
        StateProvince = stateProvince;
        Country = country ?? throw new ArgumentNullException(nameof(country));
    }

    public string Line1 { get; private set; }

    public string? Line2 { get; private set; }

    public string PostalCode { get; private set; }

    public string City { get; private set; }

    public string? StateProvince { get; private set; }

    public string Country { get; private set; }

    public void Update(string line1, string? line2, string postalCode, string city, string? stateProvince, string country)
    {
        Line1 = line1 ?? throw new ArgumentNullException(nameof(line1));
        Line2 = line2;
        PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        City = city ?? throw new ArgumentNullException(nameof(city));
        StateProvince = stateProvince;
        Country = country ?? throw new ArgumentNullException(nameof(country));
    }
}
