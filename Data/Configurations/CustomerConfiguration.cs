using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBErp.Models;
using SMBErp.Models.Enums;

namespace SMBErp.Data.Configurations;

/// <summary>
/// Entity Framework Configuration für Customer
/// </summary>
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        // Primary Key
        builder.HasKey(c => c.Id);

        // Kundennummer (eindeutig)
        builder.Property(c => c.CustomerNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(c => c.CustomerNumber)
            .IsUnique()
            .HasDatabaseName("IX_Customers_CustomerNumber");

        // Firmenname (optional)
        builder.Property(c => c.CompanyName)
            .HasMaxLength(200);

        // Kontaktperson
        builder.Property(c => c.ContactFirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.ContactLastName)
            .HasMaxLength(100)
            .IsRequired();

        // Kontaktinformationen
        builder.Property(c => c.Email)
            .HasMaxLength(200);

        builder.HasIndex(c => c.Email)
            .HasDatabaseName("IX_Customers_Email");

        builder.Property(c => c.AlternativeEmail)
            .HasMaxLength(200);

        builder.Property(c => c.Phone)
            .HasMaxLength(50);

        builder.Property(c => c.Mobile)
            .HasMaxLength(50);

        builder.Property(c => c.Fax)
            .HasMaxLength(50);

        builder.Property(c => c.Website)
            .HasMaxLength(200);

        // Rechnungsadresse (erforderlich)
        builder.Property(c => c.BillingStreet)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.BillingCity)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.BillingZipCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.BillingCountry)
            .HasMaxLength(100)
            .IsRequired()
            .HasDefaultValue("Österreich");

        // Lieferadresse (optional)
        builder.Property(c => c.ShippingStreet)
            .HasMaxLength(200);

        builder.Property(c => c.ShippingCity)
            .HasMaxLength(100);

        builder.Property(c => c.ShippingZipCode)
            .HasMaxLength(20);

        builder.Property(c => c.ShippingCountry)
            .HasMaxLength(100);

        // Steuerliche Informationen
        builder.Property(c => c.VatId)
            .HasMaxLength(30);

        builder.Property(c => c.TaxNumber)
            .HasMaxLength(50);

        // Zahlungsbedingungen
        builder.Property(c => c.PaymentTermDays)
            .HasDefaultValue(14);

        builder.Property(c => c.DiscountPercentage)
            .HasPrecision(5, 2);

        builder.Property(c => c.CreditLimit)
            .HasPrecision(18, 2);

        // Status
        builder.Property(c => c.Status)
            .HasConversion<string>()
            .HasDefaultValue(CustomerStatus.Active);

        // Timestamps
        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .IsRequired();

        builder.Property(c => c.LastContactDate);

        // Notizen
        builder.Property(c => c.Notes)
            .HasMaxLength(1000);

        // Beziehungen
        builder.HasMany(c => c.Invoices)
            .WithOne(i => i.Customer)
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes für Performance
        builder.HasIndex(c => c.Status)
            .HasDatabaseName("IX_Customers_Status");

        builder.HasIndex(c => c.CreatedAt)
            .HasDatabaseName("IX_Customers_CreatedAt");

        builder.HasIndex(c => new { c.ContactLastName, c.ContactFirstName })
            .HasDatabaseName("IX_Customers_ContactName");
    }
}
