namespace BeautyPlanner.TenantService.UnitTests.Builders;

public class SalonBuilder
{
    private int _id = 999;
    private Guid _vanityId = Guid.Empty;
    private int _tenantId = 999;
    private Guid _tenantVanityId = Guid.Empty;
    private string _name = "Default salon";
    private string? _description;
    private string _email = "default@email.cz";
    private string _phoneNumber = "987654321";
    private Address _address = new AddressBuilder().Build();
    private AddressModel _addressModel = new AddressBuilder().BuildModel();

    public SalonBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public SalonBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public SalonBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public SalonBuilder WithPhoneNumber(string proneNumber)
    {
        _phoneNumber = proneNumber;
        return this;
    }

    public SalonBuilder WithAddress(Address address)
    {
        _address = address;
        return this;
    }

    public Salon Build()
    {
        return new Salon(_name, _description, _email, _phoneNumber, _address, _tenantId);
    }

    public CreateSalonModel BuildCreateModel()
    {
        return new CreateSalonModel(_name, _description, _email, _phoneNumber, _addressModel, _tenantVanityId);
    }

    public UpdateSalonModel BuildUpdateModel()
    {
        return new UpdateSalonModel(_id, _vanityId, _name, _description, _email, _phoneNumber, _addressModel, _tenantVanityId);
    }
}
