namespace BeautyPlanner.Shared.Domain.Common;

public abstract class BaseEntity
{
    [NotMapped]
    public bool IsNew
    {
        get
        {
            return Id == 0 && VanityId == Guid.Empty;
        }
    }

    public int Id { get; set; }

    public Guid VanityId { get; private set; }

    protected BaseEntity()
    {
        VanityId = Guid.NewGuid();
    }
}
