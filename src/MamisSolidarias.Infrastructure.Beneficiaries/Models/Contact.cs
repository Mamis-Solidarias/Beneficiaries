
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

public class Contact
{
    internal int Id { get; set; }
    
    public ContactType Type { get; set; }

    public string Content { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public bool IsPreferred { get; set; }
    
}

internal class ContactModelBuilder : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> model)
    {
        model.HasKey(t => t.Id);
        
        model.Property(t => t.Id).ValueGeneratedOnAdd();
        
        model.Property(t => t.Content)
            .IsRequired()
            .HasMaxLength(100);

        model.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        model.Property(t => t.Type)
            .HasConversion<int>()
            .IsRequired();

        model.Property(t => t.IsPreferred)
            .IsRequired();
        
    }
}