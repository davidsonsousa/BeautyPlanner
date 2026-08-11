namespace BeautyPlanner.Shared.Infrastructure.Persistence.Helpers;

public static class AuditConfigurationHelper
{
    public static void AddAuditingConfiguration<TEntity>(EntityTypeBuilder<TEntity> b) where TEntity : AuditableEntity
    {
        b.Property(x => x.CreatedAt).IsRequired();
        b.Property(x => x.CreatedBy).HasMaxLength(100);

        b.Property(x => x.UpdatedAt);
        b.Property(x => x.UpdatedBy).HasMaxLength(100);

        b.Property(x => x.DeletedAt);
        b.Property(x => x.DeletedBy).HasMaxLength(100);
    }
}
