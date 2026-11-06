namespace BeautyPlanner.ClientService.UnitTests.Builders;

public class ClientBuilder
{
    private int _id = 999;
    private Guid _vanityId = Guid.Empty;
    private Guid _tenantVanityId = Guid.Empty;
    private string _firstName = "FirstName";
    private string _lastName = "LastName";
    private string _email = "default@email.cz";
    private string _phoneNumber = "987654321";
    private DateTime? _dateOfBirth = new DateTime(1990, 1, 1);
    private Address? _address = new AddressBuilder().Build();
    private AddressModel? _addressModel = new AddressBuilder().BuildModel();

    public ClientBuilder WithFirstName(string name)
    {
        _firstName = name;
        return this;
    }

    public ClientBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public ClientBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public ClientBuilder WithPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public ClientBuilder WithDateOfBirth(DateTime? dateOfBirth)
    {
        _dateOfBirth = dateOfBirth;
        return this;
    }

    public ClientBuilder WithAddress(Address? address)
    {
        _address = address;
        return this;
    }

    public Client Build()
    {
        return new Client(_firstName, _lastName, _email, _phoneNumber, _dateOfBirth, _address, _tenantVanityId);
    }

    public CreateClientModel BuildCreateModel()
    {
        return new CreateClientModel(_firstName, _lastName, _email, _phoneNumber, _dateOfBirth, _addressModel, _tenantVanityId);
    }

    public UpdateClientModel BuildUpdateModel()
    {
        return new UpdateClientModel(_id, _vanityId, _firstName, _lastName, _email, _phoneNumber, _dateOfBirth, _addressModel, _tenantVanityId);
    }

}
