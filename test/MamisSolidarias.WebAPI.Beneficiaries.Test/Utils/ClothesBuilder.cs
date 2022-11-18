using Bogus;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Utils;

internal class ClothesBuilder
{
    private static readonly Faker<Clothes> Generator = new Faker<Clothes>()
        .RuleFor(t => t.Id, f => f.UniqueIndex)
        .RuleFor(t => t.PantsSize, f => $"{f.Random.Number(3, 70)}")
        .RuleFor(t => t.ShirtSize, f => $"{f.Random.Number(3, 70)}")
        .RuleFor(t => t.ShoeSize, f =>f.Random.Int(11, 50));

    private readonly Clothes _clothes = Generator.Generate();

    public ClothesBuilder(Clothes c) => _clothes = c;
    public ClothesBuilder(){}

    public ClothesBuilder WithId(int id)
    {
        _clothes.Id = id;
        return this;
    }

    public ClothesBuilder WithPantsSize(string? pants)
    {
        _clothes.PantsSize = pants;
        return this;
    }
    
    public ClothesBuilder WithShoeSize(int? shoes)
    {
        _clothes.ShoeSize = shoes;
        return this;
    }
    public ClothesBuilder WithShirtSize(string? shirt)
    {
        _clothes.ShirtSize = shirt;
        return this;
    }

    public Clothes Build() => _clothes;

    public static implicit operator Clothes(ClothesBuilder c) => c.Build();
    public static implicit operator ClothesBuilder(Clothes c) => new(c);






}