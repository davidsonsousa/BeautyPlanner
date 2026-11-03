namespace BeautyPlanner.StaffService.UnitTests.Domain.Entities;

public class ProfessionTests
{
    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ProfessionBuilder().WithName(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_SetProperties_When_ValidValuesAreProvided()
    {
        var profession = new ProfessionBuilder().WithName("Profession 1")
                                                .Build();

        Assert.Equal("Profession 1", profession.Name);
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        var profession = new ProfessionBuilder().WithName("Profession 1")
                                                .Build();

        Assert.Throws<ArgumentNullException>(() => profession.Update(null!));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_UpdateNameAndDescription_When_ValidValuesAreProvided()
    {
        var profession = new ProfessionBuilder().WithName("Profession 1")
                                                .WithDescription("Profession description")
                                                .Build();

        var updatedName = "Updated name";
        var updatedDescription = "Updated description";

        profession.Update(updatedName, updatedDescription);

        Assert.Equal(updatedName, profession.Name);
        Assert.Equal(updatedDescription, profession.Description);
    }
}
