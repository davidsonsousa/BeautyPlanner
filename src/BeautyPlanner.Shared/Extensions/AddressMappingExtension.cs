namespace BeautyPlanner.Shared.Extensions;

public static class AddressMappingExtension
{
    public static AddressModel? ToModel(this Address? address)
    {
        return address == null
               ? null
               : new AddressModel(address.Line1, address.Line2, address.PostalCode, address.City, address.StateProvince, address.Country);
    }
}
