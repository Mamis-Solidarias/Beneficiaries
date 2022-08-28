using System;
using Bogus;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Utils;

internal class ContactBuilder
{
    private static readonly Faker<Contact> ContactGenerator = new Faker<Contact>()
            .RuleFor(t=> t.Id, f=> f.IndexFaker)
            .RuleFor(t=> t.Type, f=> f.PickRandom<ContactType>())
            .RuleFor(t=> t.Title, f=> f.Lorem.Word())
            .RuleFor(t=> t.IsPreferred, f => f.Random.Bool())
            .RuleFor(t=> t.Content,ContentGenerator)
        ;

    private static string ContentGenerator(Faker f, Contact c)
        => c.Type switch
        {
            ContactType.Phone => f.Phone.PhoneNumber("11########"),
            ContactType.Email => f.Internet.Email(),
            ContactType.Whatsapp => f.Phone.PhoneNumber("11########"),
            ContactType.Facebook => f.Internet.UserName(),
            ContactType.Instagram => f.Internet.UserName(),
            ContactType.Other => f.Lorem.Word(),
            _ => throw new ArgumentOutOfRangeException()
        };

    private Contact Contact { get; set; }

    public ContactBuilder() => Contact = ContactGenerator.Generate();
    

    public ContactBuilder(Contact contact) => Contact = contact;

    public Contact Build()
    {
        return Contact;
    }

    public ContactBuilder WithId(int id)
    {
        
        Contact.Id = id;
        return this;
    }

    public ContactBuilder WithTitle(string title)
    {
        Contact.Title = title;
        return this;
    }

    public ContactBuilder WithContent(ContactType type, string content)
    {
        Contact.Type = type;
        Contact.Content = content;
        return this;
    }

    public ContactBuilder IsPreferred(bool isPreferred = true)
    {
        Contact.IsPreferred = isPreferred;
        return this;
    }

    public static implicit operator Contact(ContactBuilder c) => c.Build();
    public static implicit operator ContactBuilder(Contact c) => new (c);
}