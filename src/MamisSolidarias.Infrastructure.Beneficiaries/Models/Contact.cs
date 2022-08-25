using System.Net.Mime;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;

public enum ContactType
{
    Phone,
    Email,
    Whatsapp,
    Facebook,
    Instagram,
    Other
}

internal class Contact
{
    private int Id { get; set; }
    
    public ContactType Type { get; set; }

    public string Content { get; set; }

    public string Title { get; set; }

    public bool IsPreferred { get; set; }
}