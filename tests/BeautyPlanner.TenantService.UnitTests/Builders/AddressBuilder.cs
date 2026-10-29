namespace BeautyPlanner.TenantService.UnitTests.Builders;

public class AddressBuilder
{
    private string _line1 = "Default street 1000/10";
    private string? _line2;
    private string _postalCode = "110 10";
    private string _city = "Praha";
    private string? _stateProvince;
    private string _country = "CZ";

    public AddressBuilder WithLine1(string line1)
    {
        _line1 = line1;
        return this;
    }

    public AddressBuilder WithLine2(string? line2)
    {
        _line2 = line2;
        return this;
    }

    public AddressBuilder WithPostalCode(string postalCode)
    {
        _postalCode = postalCode;
        return this;
    }

    public AddressBuilder WithCity(string city)
    {
        _city = city;
        return this;
    }

    public AddressBuilder WithStateProvince(string? stateProvince)
    {
        _stateProvince = stateProvince;
        return this;
    }

    public AddressBuilder WithCountry(string country)
    {
        _country = country;
        return this;
    }

    public Address Build()
    {
        return new Address(_line1, _line2, _postalCode, _city, _stateProvince, _country);
    }
}
