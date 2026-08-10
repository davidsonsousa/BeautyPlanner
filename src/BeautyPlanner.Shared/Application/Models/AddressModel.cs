namespace BeautyPlanner.Shared.Application.Models;

public record AddressModel(
    string Line1,
    string? Line2,
    string PostalCode,
    string City,
    string? StateProvince,
    string Country
);
