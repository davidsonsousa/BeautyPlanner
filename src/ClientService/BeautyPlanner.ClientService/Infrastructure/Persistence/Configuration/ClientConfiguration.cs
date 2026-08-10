namespace BeautyPlanner.ClientService.Infrastructure.Persistence.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure (EntityTypeBuilder<Client> b)
    {
        // Key
        b.HasKey(model => model.Id);

        // Properties
        b.Property(model => model.FirstName)
            .HasMaxLength(150)
            .IsRequired();

        b.Property(model => model.LastName)
            .HasMaxLength(150)
            .IsRequired();

        b.Property(model => model.Email)
            .HasMaxLength(150)
            .IsRequired();

        b.Property(model => model.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

        b.Property(model => model.DateOfBirth)
            .HasMaxLength(9);

        AuditConfigurationHelper.AddAuditingConfiguration(b);
    }
}
