namespace BeautyPlanner.TenantService.UnitTests.Builders;

public class TenantBuilder
{
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
}
