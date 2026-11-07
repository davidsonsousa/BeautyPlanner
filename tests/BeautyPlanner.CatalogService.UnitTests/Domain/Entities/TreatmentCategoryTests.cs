namespace BeautyPlanner.CatalogService.UnitTests.Domain.Entities;

public class TreatmentCategoryTests
{
    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new TreatmentCategoryBuilder().WithName(null!).Build());
    }

    [Trait("Category", "Constructor")]
    [Fact]
    public void Constructor_Should_SetProperties_When_ValidValuesAreProvided()
    {
        var treatmentCategory = new TreatmentCategoryBuilder().WithName("TreatmentCategory 1")
                                        .Build();

        Assert.Equal("TreatmentCategory 1", treatmentCategory.Name);
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_ThrowArgumentNullException_When_NameIsNull()
    {
        var treatmentCategory = new TreatmentCategoryBuilder().WithName("TreatmentCategory 1")
                                        .Build();

        Assert.Throws<ArgumentNullException>(() => treatmentCategory.Update(null!));
    }

    [Trait("Category", "Update")]
    [Fact]
    public void Update_Should_UpdateNameAndDescription_When_ValidValuesAreProvided()
    {
        var treatmentCategory = new TreatmentCategoryBuilder().WithName("TreatmentCategory 1")
                                        .WithDescription("TreatmentCategory description")
                                        .Build();

        var updatedName = "Updated name";
        var updatedDescription = "Updated description";

        treatmentCategory.Update(updatedName, updatedDescription);

        Assert.Equal(updatedName, treatmentCategory.Name);
        Assert.Equal(updatedDescription, treatmentCategory.Description);
    }
}
