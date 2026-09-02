namespace BeautyPlanner.CatalogService.Domain.Entities;

public class TreatmentCategory : AuditableEntity
{
    public TreatmentCategory()
    {

    }

    public TreatmentCategory(string name, string? description)
    {
        SetPropertyValues(name, description);
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }


    // Navigation properties
    public virtual ICollection<Treatment> Treatments { get; set; } = null!;


    public void Update(string name, string? description)
    {
        SetPropertyValues(name, description);
    }

    private void SetPropertyValues(string name, string? description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
    }
}
