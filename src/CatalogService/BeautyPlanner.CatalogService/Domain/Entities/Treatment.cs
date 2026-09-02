namespace BeautyPlanner.CatalogService.Domain.Entities;

public class Treatment : AuditableEntity
{
    public Treatment()
    {

    }

    public Treatment(string name, string? description, decimal price, int durationInMinutes, Guid salonVanityId, Guid professionVanityId, int treatmentCategoryId)
    {
        SetPropertyValues(name, description, price, durationInMinutes);

        SalonVanityId = salonVanityId;
        ProfessionVanityId = professionVanityId;
        TreatmentCategoryId = treatmentCategoryId;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public int DurationInMinutes { get; private set; }


    // Navigation properties
    public int TreatmentCategoryId { get; private set; }

    public virtual TreatmentCategory TreatmentCategory { get; private set; } = null!;

    public Guid SalonVanityId { get; private set; }

    public Guid ProfessionVanityId { get; private set; }

    public void Update(string name, string? description, decimal price, int durationInMinutes, Guid professionVanityId, int treatmentCategoryId)
    {
        SetPropertyValues(name, description, price, durationInMinutes);

        ProfessionVanityId = professionVanityId;
        TreatmentCategoryId = treatmentCategoryId;
    }

    private void SetPropertyValues(string name, string? description, decimal price, int durationInMinutes)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Price = price >= 0 ? price : throw new ArgumentException("Price cannot be negative.");
        DurationInMinutes = durationInMinutes > 0 ? durationInMinutes : throw new ArgumentException("Duration must be positive.");
    }
}
