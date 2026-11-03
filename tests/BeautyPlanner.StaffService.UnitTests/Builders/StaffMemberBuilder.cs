namespace BeautyPlanner.StaffService.UnitTests.Builders;

public class StaffMemberBuilder
{
    private int _id = 999;
    private Guid _vanityId = Guid.Empty;
    private int _professionId = 999;
    private Guid _professionVanityId = Guid.Empty;
    private Guid _salonVanityId = Guid.Empty;
    private string _firstName = "FirstName";
    private string _lastName = "LastName";
    private string _email = "default@email.cz";
    private string _phoneNumber = "987654321";
    private DateTime? _dateOfBirth = new DateTime(1990, 1, 1);
    private Address? _address = new AddressBuilder().Build();
    private AddressModel? _addressModel = new AddressBuilder().BuildModel();

    public StaffMemberBuilder WithFirstName(string name)
    {
        _firstName = name;
        return this;
    }

    public StaffMemberBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public StaffMemberBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public StaffMemberBuilder WithPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public StaffMemberBuilder WithDateOfBirth(DateTime? dateOfBirth)
    {
        _dateOfBirth = dateOfBirth;
        return this;
    }

    public StaffMemberBuilder WithAddress(Address? address)
    {
        _address = address;
        return this;
    }

    public StaffMember Build()
    {
        return new StaffMember(_firstName, _lastName, _email, _phoneNumber, _professionId, _dateOfBirth, _address, _salonVanityId);
    }

    public CreateStaffMemberModel BuildCreateModel()
    {
        return new CreateStaffMemberModel(_firstName, _lastName, _email, _phoneNumber, _professionVanityId, _dateOfBirth, _addressModel, _salonVanityId);
    }

    public UpdateStaffMemberModel BuildUpdateModel()
    {
        return new UpdateStaffMemberModel(_id, _vanityId, _firstName, _lastName, _email, _phoneNumber, _professionVanityId, _dateOfBirth, _addressModel, _salonVanityId);
    }
}
