namespace BeautyPlanner.StaffService.Domain.Entities;

public class Profession : AuditableEntity
{
    public Profession(string name, string? description = null)
    {
        SetPropertyValues(name, description);
    }

    public string Name { get; set; }

    public string? Description { get; set; }


    // Navigation properties
    public int StaffMemberId { get; set; }

    public ICollection<StaffMember> StaffMembers { get; set; } = [];

    public void Update(string name, string? description = null)
    {
        SetPropertyValues(name, description);
    }

    private void SetPropertyValues(string name, string? description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
    }
}
