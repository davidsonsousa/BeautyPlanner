namespace BeautyPlanner.TenantService.Domain.Entities;

public class Tenant : AuditableEntity
{
    public Tenant(string name, string? description = null)
    {
        SetPropertyValues(name, description);
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }


    // Navigation properties
    public virtual ICollection<Salon> Salons { get; set; } = null!;

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
