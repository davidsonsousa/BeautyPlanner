namespace BeautyPlanner.TenantService.UnitTests.Domain.Entities;

public class SalonTests
{
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new SalonBuilder().WithName(null!).Build());
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_EmailIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new SalonBuilder().WithEmail(null!).Build());
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_PhoneNumberIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new SalonBuilder().WithPhoneNumber(null!).Build());
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_AddressIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new SalonBuilder().WithAddress(null!).Build());
    }

    [Fact]
    public void Constructor_Should_SetProperties_When_ValidValuesAreProvided()
    {
        var salon = new SalonBuilder().WithName("Salon 1")
                                      .WithDescription("Description")
                                      .WithEmail("salon@email.cz")
                                      .WithPhoneNumber("456123789")
                                      .WithAddress(new AddressBuilder().WithLine1("Streetova 2000/20").Build())
                                      .Build();

        Assert.Equal("Salon 1", salon.Name);
        Assert.Equal("Description", salon.Description);
        Assert.Equal("salon@email.cz", salon.Email);
        Assert.Equal("456123789", salon.PhoneNumber);
        Assert.Equal("Streetova 2000/20", salon.Address.Line1);
    }

    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        var salon = new SalonBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => salon.Update(null!, "Description", "salon@email.cz", "456123789",
                                                                new AddressBuilder().Build()));
    }

    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_EmailIsNull()
    {
        var salon = new SalonBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => salon.Update("Salon 1", "Description", null!, "456123789",
                                                                new AddressBuilder().Build()));
    }

    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_PhoneNumberIsNull()
    {
        var salon = new SalonBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => salon.Update("Salon 1", "Description", "salon@email.cz", null!,
                                                                new AddressBuilder().Build()));
    }

    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_AddressIsNull()
    {
        var salon = new SalonBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => salon.Update("Salon 1", "Description", "salon@email.cz", "456123789",null!));
    }

    [Fact]
    public void Update_Should_UpdateAllValues_When_ValidValuesAreProvided()
    {
        var salon = new SalonBuilder().Build();

        salon.Update("Salon 1", "Description", "salon@email.cz", "456123789", new AddressBuilder().WithLine1("Streetova 2000/20").Build());

        Assert.Equal("Salon 1", salon.Name);
        Assert.Equal("Description", salon.Description);
        Assert.Equal("salon@email.cz", salon.Email);
        Assert.Equal("456123789", salon.PhoneNumber);
        Assert.Equal("Streetova 2000/20", salon.Address.Line1);
    }
}
