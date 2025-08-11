using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBErp.Domain.Inventory;
using SMBErp.Domain.Shared;

namespace SMBErp.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Konfiguration für die abstrakte Item-Basisklasse
/// </summary>
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        // Table Per Hierarchy (TPH) Konfiguration
        builder.ToTable("Items");

        // Discriminator für Vererbung
        builder.HasDiscriminator<string>("ItemType")
            .HasValue<Product>("Product")
            .HasValue<Service>("Service");

        // Primary Key
        builder.HasKey(i => i.Id);

        // Eindeutige Indizes
        builder.HasIndex(i => i.ItemNumber)
            .IsUnique()
            .HasDatabaseName("IX_Items_ItemNumber");

        // String-Eigenschaften
        builder.Property(i => i.ItemNumber)
            .IsRequired()
            .HasMaxLength(50)
            .HasComment("Eindeutige Artikelnummer");

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("Artikelname");

        builder.Property(i => i.Description)
            .HasMaxLength(1000)
            .HasComment("Artikelbeschreibung");

        builder.Property(i => i.Category)
            .HasMaxLength(100)
            .HasComment("Artikelkategorie");

        builder.Property(i => i.Unit)
            .HasDefaultValue(Unit.Piece)
            .HasComment("Mengeneinheit");

        // Preise mit Precision
        builder.Property(i => i.SalePrice)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasComment("Verkaufspreis (netto)");

        builder.Property(i => i.PurchasePrice)
            .HasPrecision(18, 2)
            .HasComment("Einkaufspreis (netto)");

        // MwSt-Satz
        builder.Property(i => i.VatRate)
            .HasPrecision(5, 2)
            .HasDefaultValue(19.00m)
            .HasComment("Mehrwertsteuersatz in Prozent");

        // Enum-Konfiguration - entfernt da Status nicht in Item-Model vorhanden

        // Zeitstempel
        builder.Property(i => i.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasComment("Erstellungsdatum");

        builder.Property(i => i.UpdatedAt)
            .HasComment("Letzte Änderung");

        // Navigation Properties
        builder.HasMany(i => i.InvoiceItems)
            .WithOne(ii => ii.Item)
            .HasForeignKey(ii => ii.ItemId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_InvoiceItems_Item");

        // Indizes für Performance
        builder.HasIndex(i => i.Name)
            .HasDatabaseName("IX_Items_Name");

        builder.HasIndex(i => i.Category)
            .HasDatabaseName("IX_Items_Category");

        builder.HasIndex(i => i.SalePrice)
            .HasDatabaseName("IX_Items_SalePrice");

        builder.HasIndex(i => i.CreatedAt)
            .HasDatabaseName("IX_Items_CreatedAt");
    }
}
