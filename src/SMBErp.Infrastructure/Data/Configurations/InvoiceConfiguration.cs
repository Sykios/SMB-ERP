using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBErp.Domain.Sales;
using SMBErp.Domain.Shared;

namespace SMBErp.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Configuration für Invoice
/// </summary>
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        // Primary Key
        builder.HasKey(i => i.Id);

        // Rechnungsnummer (eindeutig)
        builder.Property(i => i.InvoiceNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(i => i.InvoiceNumber)
            .IsUnique()
            .HasDatabaseName("IX_Invoices_InvoiceNumber");

        // Customer Referenz
        builder.Property(i => i.CustomerId)
            .IsRequired();

        // Datums-Felder
        builder.Property(i => i.InvoiceDate)
            .IsRequired();

        builder.Property(i => i.DueDate)
            .IsRequired();

        builder.Property(i => i.ServiceDate);

        // Status
        builder.Property(i => i.Status)
            .HasConversion<string>()
            .HasDefaultValue(InvoiceStatus.Draft);

        // Geldbeträge
        builder.Property(i => i.NetAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.VatAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(i => i.PaidAmount)
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        // Skonto
        builder.Property(i => i.DiscountPercentage)
            .HasPrecision(5, 2);

        builder.Property(i => i.DiscountDays);

        builder.Property(i => i.PaymentTermDays)
            .HasDefaultValue(14);

        // Texte
        builder.Property(i => i.Subject)
            .HasMaxLength(200);

        builder.Property(i => i.IntroductionText)
            .HasMaxLength(1000);

        builder.Property(i => i.ConclusionText)
            .HasMaxLength(1000);

        builder.Property(i => i.InternalNotes)
            .HasMaxLength(1000);

        builder.Property(i => i.CustomerReference)
            .HasMaxLength(100);

        builder.Property(i => i.ProjectNumber)
            .HasMaxLength(50);

        // Timestamps
        builder.Property(i => i.CreatedAt)
            .IsRequired();

        builder.Property(i => i.UpdatedAt)
            .IsRequired();

        builder.Property(i => i.SentDate);

        builder.Property(i => i.PaidDate);

        // Beziehungen
        builder.HasOne(i => i.Customer)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(i => i.InvoiceItems)
            .WithOne(ii => ii.Invoice)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes für Performance
        builder.HasIndex(i => i.Status)
            .HasDatabaseName("IX_Invoices_Status");

        builder.HasIndex(i => i.InvoiceDate)
            .HasDatabaseName("IX_Invoices_InvoiceDate");

        builder.HasIndex(i => i.DueDate)
            .HasDatabaseName("IX_Invoices_DueDate");

        builder.HasIndex(i => i.CustomerId)
            .HasDatabaseName("IX_Invoices_CustomerId");
    }
}
