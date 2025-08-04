using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBErp.Models;

namespace SMBErp.Data.Configurations;

/// <summary>
/// Entity Framework Konfiguration für die Company-Entität
/// </summary>
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // Tabellenname
        builder.ToTable("Companies");

        // Primary Key
        builder.HasKey(c => c.Id);

        // Firmeninformationen
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("Firmenname");

        builder.Property(c => c.LegalForm)
            .HasMaxLength(50)
            .HasComment("Rechtsform (GmbH, AG, etc.)");

        // Adressinformationen
        builder.Property(c => c.Street)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("Straße und Hausnummer");

        builder.Property(c => c.City)
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("Stadt");

        builder.Property(c => c.ZipCode)
            .IsRequired()
            .HasMaxLength(20)
            .HasComment("Postleitzahl");

        builder.Property(c => c.Country)
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("Land");

        // Kontaktinformationen
        builder.Property(c => c.Phone)
            .HasMaxLength(50)
            .HasComment("Telefonnummer");

        builder.Property(c => c.Fax)
            .HasMaxLength(50)
            .HasComment("Faxnummer");

        builder.Property(c => c.Email)
            .HasMaxLength(255)
            .HasComment("E-Mail-Adresse");

        builder.Property(c => c.Website)
            .HasMaxLength(255)
            .HasComment("Website URL");

        // Finanzinformationen
        builder.Property(c => c.TaxNumber)
            .HasMaxLength(50)
            .HasComment("Steuernummer");

        builder.Property(c => c.VatId)
            .HasMaxLength(50)
            .HasComment("Umsatzsteuer-Identifikationsnummer");

        builder.Property(c => c.IBAN)
            .HasMaxLength(34)
            .HasComment("IBAN Bankkonto");

        builder.Property(c => c.BIC)
            .HasMaxLength(11)
            .HasComment("BIC/SWIFT Code");

        // Geschäftseinstellungen
        builder.Property(c => c.DefaultPaymentTermDays)
            .HasDefaultValue(30)
            .HasComment("Standard Zahlungsziel in Tagen");

        builder.Property(c => c.DefaultVatRate)
            .HasPrecision(5, 2)
            .HasDefaultValue(19.00m)
            .HasComment("Standard Mehrwertsteuersatz in Prozent");

        // Indizierung für bessere Performance
        builder.HasIndex(c => c.Name)
            .HasDatabaseName("IX_Companies_Name");

        builder.HasIndex(c => c.Email)
            .HasDatabaseName("IX_Companies_Email");

        builder.HasIndex(c => c.TaxNumber)
            .HasDatabaseName("IX_Companies_TaxNumber");

        builder.HasIndex(c => c.VatId)
            .HasDatabaseName("IX_Companies_VatId");
    }
}