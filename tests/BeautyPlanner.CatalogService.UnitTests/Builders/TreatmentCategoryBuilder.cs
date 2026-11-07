namespace BeautyPlanner.CatalogService.UnitTests.Builders;

public class TreatmentCategoryBuilder
{
    private int _id = 999;
    private Guid _vanityId = Guid.Empty;
    private string _name = "Default treatment category";
    private string? _description;

    public TreatmentCategoryBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TreatmentCategoryBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public TreatmentCategory Build()
    {
        return new TreatmentCategory(_name, _description);
    }

    public CreateTreatmentCategoryModel BuildCreateModel()
    {
        return new CreateTreatmentCategoryModel(_name, _description);
    }

    public UpdateTreatmentCategoryModel BuildUpdateModel()
    {
        return new UpdateTreatmentCategoryModel(_id, _vanityId, _name, _description);
    }
}
