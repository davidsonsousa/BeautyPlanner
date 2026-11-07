namespace BeautyPlanner.CatalogService.UnitTests.Builders;

public class TreatmentBuilder
{
    private int _id = 999;
    private Guid _vanityId = Guid.Empty;
    private int _treatmentCategoryId = 999;
    private Guid _treatmentCategoryVanityId = Guid.Empty;
    private TreatmentCategory _treatmentCategory = new TreatmentCategoryBuilder().Build();
    private Guid _professionVanityId = Guid.Empty;
    private Guid _salonVanityId = Guid.Empty;
    private string _name = "Default treatment";
    private string? _description;
    private decimal _price = 50M;
    private int _durationInMinutes = 30;

    public TreatmentBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TreatmentBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public TreatmentBuilder WithPrice(decimal price)
    {
        _price = price;
        return this;
    }

    public TreatmentBuilder WithDurationInMinutes(int durationInMinutes)
    {
        _durationInMinutes = durationInMinutes;
        return this;
    }

    public Treatment Build()
    {
        return new Treatment(_name, _description, _price, _durationInMinutes, _salonVanityId, _professionVanityId, _treatmentCategoryId)
        {
            TreatmentCategory = _treatmentCategory
        };
    }

    public CreateTreatmentModel BuildCreateModel()
    {
        return new CreateTreatmentModel(_name, _description, _price, _durationInMinutes, _salonVanityId, _professionVanityId, _treatmentCategoryVanityId);
    }

    public UpdateTreatmentModel BuildUpdateModel()
    {
        return new UpdateTreatmentModel(_id, _vanityId, _name, _description, _price, _durationInMinutes, _professionVanityId, _treatmentCategoryVanityId);
    }
}
