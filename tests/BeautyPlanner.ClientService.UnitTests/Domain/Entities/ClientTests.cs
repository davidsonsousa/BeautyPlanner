namespace BeautyPlanner.ClientService.UnitTests.Domain.Entities;

public class ClientTests
{
    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_FirstNameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ClientBuilder().WithFirstName(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_LastNameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ClientBuilder().WithLastName(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_EmailIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ClientBuilder().WithEmail(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_PhoneNumberIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ClientBuilder().WithPhoneNumber(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_AddressIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ClientBuilder().WithAddress(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_SetProperties_When_ValidValuesAreProvided()
    {
        var client = new ClientBuilder().WithFirstName("Client")
                                        .WithLastName("Member")
                                        .WithEmail("client.member@email.cz")
                                        .WithPhoneNumber("456123789")
                                        .WithAddress(new AddressBuilder().WithLine1("Streetova 2000/20").Build())
                                        .Build();

        Assert.Equal("Client", client.FirstName);
        Assert.Equal("Member", client.LastName);
        Assert.Equal("Client Member", client.FullName);
        Assert.Equal("client.member@email.cz", client.Email);
        Assert.Equal("456123789", client.PhoneNumber);
        Assert.NotNull(client.Address);
        Assert.Equal("Streetova 2000/20", client.Address.Line1);
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_FirstNameIsNull()
    {
        var client = new ClientBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => client.Update(null!, "LastName", "client.member@email.cz", "456123789",
                                                                 client.DateOfBirth, new AddressBuilder().Build()));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_LastNameIsNull()
    {
        var client = new ClientBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => client.Update("FirstName", null!, "client.member@email.cz", "456123789",
                                                                 client.DateOfBirth, new AddressBuilder().Build()));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_EmailIsNull()
    {
        var client = new ClientBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => client.Update("Client", "Member", null!, "456123789",
                                                                 client.DateOfBirth, new AddressBuilder().Build()));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_PhoneNumberIsNull()
    {
        var client = new ClientBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => client.Update("Client", "Member", "client.member@email.cz", null!,
                                                                 client.DateOfBirth, new AddressBuilder().Build()));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_AddressIsNull()
    {
        var client = new ClientBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => client.Update("Client", "Member", "client.member@email.cz", "456123789",
                                                                 client.DateOfBirth, null));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_UpdateAllValues_When_ValidValuesAreProvided()
    {
        var client = new ClientBuilder().Build();

        client.Update("Client", "Member", "client.member@email.cz", "456123789", new DateTime(2000, 1, 1),
                      new AddressBuilder().WithLine1("Streetova 2000/20").Build());

        Assert.Equal("Client", client.FirstName);
        Assert.Equal("Member", client.LastName);
        Assert.Equal("Client Member", client.FullName);
        Assert.Equal("client.member@email.cz", client.Email);
        Assert.Equal("456123789", client.PhoneNumber);
        Assert.Equal(new DateTime(2000, 1, 1), client.DateOfBirth);
        Assert.NotNull(client.Address);
        Assert.Equal("Streetova 2000/20", client.Address.Line1);
    }
}
