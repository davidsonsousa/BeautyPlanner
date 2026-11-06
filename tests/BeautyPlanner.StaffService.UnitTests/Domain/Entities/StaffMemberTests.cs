namespace BeautyPlanner.StaffService.UnitTests.Domain.Entities;

public class StaffMemberTests
{
    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_FirstNameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new StaffMemberBuilder().WithFirstName(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_LastNameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new StaffMemberBuilder().WithLastName(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_EmailIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new StaffMemberBuilder().WithEmail(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_PhoneNumberIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new StaffMemberBuilder().WithPhoneNumber(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_AddressIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new StaffMemberBuilder().WithAddress(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_SetProperties_When_ValidValuesAreProvided()
    {
        var staffMember = new StaffMemberBuilder().WithFirstName("Staff")
                                                  .WithLastName("Member")
                                                  .WithEmail("staff.member@email.cz")
                                                  .WithPhoneNumber("456123789")
                                                  .WithAddress(new AddressBuilder().WithLine1("Streetova 2000/20").Build())
                                                  .Build();

        Assert.Equal("Staff", staffMember.FirstName);
        Assert.Equal("Member", staffMember.LastName);
        Assert.Equal("Staff Member", staffMember.FullName);
        Assert.Equal("staff.member@email.cz", staffMember.Email);
        Assert.Equal("456123789", staffMember.PhoneNumber);
        Assert.NotNull(staffMember.Address);
        Assert.Equal("Streetova 2000/20", staffMember.Address.Line1);
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_FirstNameIsNull()
    {
        var staffMember = new StaffMemberBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => staffMember.Update(null!, "LastName", "staff.member@email.cz", "456123789",
                                                                      staffMember.ProfessionId, staffMember.DateOfBirth,
                                                                      new AddressBuilder().Build()));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_LastNameIsNull()
    {
        var staffMember = new StaffMemberBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => staffMember.Update("FirstName", null!, "staff.member@email.cz", "456123789",
                                                                      staffMember.ProfessionId, staffMember.DateOfBirth,
                                                                      new AddressBuilder().Build()));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_EmailIsNull()
    {
        var staffMember = new StaffMemberBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => staffMember.Update("Staff", "Member", null!, "456123789",
                                                                      staffMember.ProfessionId, staffMember.DateOfBirth,
                                                                      new AddressBuilder().Build()));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_PhoneNumberIsNull()
    {
        var staffMember = new StaffMemberBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => staffMember.Update("Staff", "Member", "staff.member@email.cz", null!,
                                                                      staffMember.ProfessionId, staffMember.DateOfBirth,
                                                                      new AddressBuilder().Build()));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_AddressIsNull()
    {
        var staffMember = new StaffMemberBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => staffMember.Update("Staff", "Member", "staff.member@email.cz", "456123789",
                                                                      staffMember.ProfessionId, staffMember.DateOfBirth,
                                                                      null));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_UpdateAllValues_When_ValidValuesAreProvided()
    {
        var staffMember = new StaffMemberBuilder().Build();

        staffMember.Update("Staff", "Member", "staff.member@email.cz", "456123789", staffMember.ProfessionId, new DateTime(2000, 1, 1),
                           new AddressBuilder().WithLine1("Streetova 2000/20").Build());

        Assert.Equal("Staff", staffMember.FirstName);
        Assert.Equal("Member", staffMember.LastName);
        Assert.Equal("Staff Member", staffMember.FullName);
        Assert.Equal("staff.member@email.cz", staffMember.Email);
        Assert.Equal("456123789", staffMember.PhoneNumber);
        Assert.Equal(new DateTime(2000, 1, 1), staffMember.DateOfBirth);
        Assert.NotNull(staffMember.Address);
        Assert.Equal("Streetova 2000/20", staffMember.Address.Line1);
    }
}
