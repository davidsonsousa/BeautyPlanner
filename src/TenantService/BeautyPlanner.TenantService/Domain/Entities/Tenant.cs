namespace BeautyPlanner.TenantService.Domain.Entities;

public class Tenant : AuditableEntity
{
    public Tenant(string name, string? description = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }


    // Navigation properties
    public virtual ICollection<Salon> Salons { get; set; } = null!;


    public void Update(string name, string? description = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
    }
}
