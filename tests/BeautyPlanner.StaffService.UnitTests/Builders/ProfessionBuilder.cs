namespace BeautyPlanner.StaffService.UnitTests.Builders;

public class ProfessionBuilder
{
    private int _id = 999;
    private Guid _vanityId = Guid.Empty;
    private int _staffMemberId = 999;
    private Guid _staffMemberVanityId = Guid.Empty;
    private string _name = "Default profession";
    private string? _description;

    public ProfessionBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProfessionBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public Profession Build()
    {
        return new Profession(_name, _description);
    }

    public CreateProfessionModel BuildCreateModel()
    {
        return new CreateProfessionModel(_name, _description);
    }

    public UpdateProfessionModel BuildUpdateModel()
    {
        return new UpdateProfessionModel(_id, _vanityId, _name, _description);
    }
}
