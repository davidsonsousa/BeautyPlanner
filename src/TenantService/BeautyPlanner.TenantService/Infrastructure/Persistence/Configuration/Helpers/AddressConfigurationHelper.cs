namespace BeautyPlanner.TenantService.Infrastructure.Persistence.Configuration.Helpers;

public static class AddressConfigurationHelper
{
    public static void AddAddressConfiguration<TEntity>(OwnedNavigationBuilder<TEntity, Address> a) where TEntity : class
    {
        a.Property(model => model.Line1)
            .HasMaxLength(150)
            .IsRequired();

        a.Property(model => model.Line2)
            .HasMaxLength(150);

        a.Property(model => model.PostalCode)
            .HasMaxLength(20)
            .IsRequired();

        a.Property(model => model.City)
            .HasMaxLength(100)
            .IsRequired();

        a.Property(model => model.StateProvince)
            .HasMaxLength(100);

        a.Property(model => model.Country)
            .HasMaxLength(2)
            .IsRequired();
    }
}
