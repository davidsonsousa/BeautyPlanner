namespace BeautyPlanner.TenantService.UnitTests.Builders;

public class TenantBuilder
{
    private int _id = 999;
    private Guid _vanityId = Guid.Empty;
    private string _name = "Default tenant";
    private string? _description;

    public TenantBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TenantBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public Tenant Build()
    {
        return new Tenant(_name, _description);
    }

    public CreateTenantModel BuildCreateModel()
    {
        return new CreateTenantModel(_name, _description);
    }

    public UpdateTenantModel BuildUpdateModel()
    {
        return new UpdateTenantModel(_id, _vanityId, _name, _description);
    }
}
