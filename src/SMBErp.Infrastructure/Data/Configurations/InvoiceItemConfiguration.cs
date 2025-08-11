using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBErp.Domain.Sales;
using SMBErp.Domain.Shared;

namespace SMBErp.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Konfiguration für die InvoiceItem-Entität
/// </summary>
public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        // Tabellenname
        builder.ToTable("InvoiceItems");

        // Primary Key
        builder.HasKey(ii => ii.Id);

        // String-Eigenschaften
        builder.Property(ii => ii.Description)
            .IsRequired()
            .HasMaxLength(500)
            .HasComment("Positionsbeschreibung");

        builder.Property(ii => ii.Unit)
            .HasDefaultValue(Unit.Piece)
            .HasComment("Mengeneinheit");

        // Numerische Eigenschaften mit Precision
        builder.Property(ii => ii.Quantity)
            .HasPrecision(18, 4)
            .IsRequired()
            .HasComment("Menge");

        builder.Property(ii => ii.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasComment("Einzelpreis (netto)");

        builder.Property(ii => ii.VatRate)
            .HasPrecision(5, 2)
            .IsRequired()
            .HasComment("Mehrwertsteuersatz in Prozent");

        // VatAmount und TotalPrice sind computed properties und werden nicht gemappt

        // Position in der Rechnung
        builder.Property(ii => ii.Position)
            .IsRequired()
            .HasComment("Positionsnummer in der Rechnung");

        // Foreign Keys
        builder.Property(ii => ii.InvoiceId)
            .IsRequired()
            .HasComment("Rechnungs-ID");

        builder.Property(ii => ii.ItemId)
            .HasComment("Artikel-ID (optional)");

        // Navigation Properties
        builder.HasOne(ii => ii.Invoice)
            .WithMany(i => i.InvoiceItems)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_InvoiceItems_Invoice");

        builder.HasOne(ii => ii.Item)
            .WithMany(i => i.InvoiceItems)
            .HasForeignKey(ii => ii.ItemId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_InvoiceItems_Item");

        // Indizes für Performance
        builder.HasIndex(ii => ii.InvoiceId)
            .HasDatabaseName("IX_InvoiceItems_InvoiceId");

        builder.HasIndex(ii => ii.ItemId)
            .HasDatabaseName("IX_InvoiceItems_ItemId");

        builder.HasIndex(ii => new { ii.InvoiceId, ii.Position })
            .IsUnique()
            .HasDatabaseName("IX_InvoiceItems_Invoice_Position");

        // Index für Berichte und Analysen
        builder.HasIndex(ii => ii.UnitPrice)
            .HasDatabaseName("IX_InvoiceItems_UnitPrice");

        // TotalPrice Index entfernt, da es eine computed property ist

        builder.HasIndex(ii => ii.VatRate)
            .HasDatabaseName("IX_InvoiceItems_VatRate");
    }
}
