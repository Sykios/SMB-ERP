using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBErp.Domain.Common;
using SMBErp.Domain.Shared;

namespace SMBErp.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Configuration für EmailTemplate
/// </summary>
public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.ToTable("EmailTemplates");

        // Primary Key
        builder.HasKey(e => e.Id);

        // Vorlagenname
        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(e => e.Name)
            .IsUnique()
            .HasDatabaseName("IX_EmailTemplates_Name");

        // Typ
        builder.Property(e => e.Type)
            .HasConversion<string>()
            .IsRequired();

        // E-Mail-Inhalte
        builder.Property(e => e.Subject)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Body)
            .IsRequired();

        builder.Property(e => e.IsHtml)
            .HasDefaultValue(true);

        builder.Property(e => e.IsActive)
            .HasDefaultValue(true);

        // Timestamps
        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        // Indexes
        builder.HasIndex(e => e.Type)
            .HasDatabaseName("IX_EmailTemplates_Type");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_EmailTemplates_IsActive");
    }
}
