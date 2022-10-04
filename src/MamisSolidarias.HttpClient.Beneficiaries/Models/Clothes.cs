namespace MamisSolidarias.HttpClient.Beneficiaries.Models;

/// <param name="Shoes">Shoe size</param>
/// <param name="Shirt">Shirt size</param>
/// <param name="Pants">Pants size</param>
public sealed record Clothes(string? Shoes, string? Shirt, string? Pants);