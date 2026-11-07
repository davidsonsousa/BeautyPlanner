namespace BeautyPlanner.CatalogService.UnitTests.Domain.Entities;

public class TreatmentTests
{
    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new TreatmentBuilder().WithName(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_SetProperties_When_ValidValuesAreProvided()
    {
        var treatment = new TreatmentBuilder().WithName("Treatment 1")
                                      .WithDescription("Description")
                                      .WithPrice(75)
                                      .WithDurationInMinutes(45)
                                      .Build();

        Assert.Equal("Treatment 1", treatment.Name);
        Assert.Equal("Description", treatment.Description);
        Assert.Equal(75, treatment.Price);
        Assert.Equal(45, treatment.DurationInMinutes);
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        var treatment = new TreatmentBuilder().Build();

        Assert.Throws<ArgumentNullException>(() => treatment.Update(null!, "Description", 75, 45, treatment.ProfessionVanityId, treatment.TreatmentCategoryId));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_UpdateAllValues_When_ValidValuesAreProvided()
    {
        var treatment = new TreatmentBuilder().Build();

        treatment.Update("Treatment 1", "Description", 75, 45, treatment.ProfessionVanityId, treatment.TreatmentCategoryId);

        Assert.Equal("Treatment 1", treatment.Name);
        Assert.Equal("Description", treatment.Description);
        Assert.Equal(75, treatment.Price);
        Assert.Equal(45, treatment.DurationInMinutes);
    }
}
