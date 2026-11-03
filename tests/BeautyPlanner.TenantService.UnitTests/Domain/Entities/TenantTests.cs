namespace BeautyPlanner.TenantService.UnitTests.Domain.Entities;

public class TenantTests
{
    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new TenantBuilder().WithName(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_SetProperties_When_ValidValuesAreProvided()
    {
        var tenant = new TenantBuilder().WithName("Tenant 1")
                                        .Build();

        Assert.Equal("Tenant 1", tenant.Name);
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        var tenant = new TenantBuilder().WithName("Tenant 1")
                                        .Build();

        Assert.Throws<ArgumentNullException>(() => tenant.Update(null!));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_UpdateNameAndDescription_When_ValidValuesAreProvided()
    {
        var tenant = new TenantBuilder().WithName("Tenant 1")
                                        .WithDescription("Tenant description")
                                        .Build();

        var updatedName = "Updated name";
        var updatedDescription = "Updated description";

        tenant.Update(updatedName, updatedDescription);

        Assert.Equal(updatedName, tenant.Name);
        Assert.Equal(updatedDescription, tenant.Description);
    }
}
