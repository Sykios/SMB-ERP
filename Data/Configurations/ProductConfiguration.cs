using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBErp.Models;

namespace SMBErp.Data.Configurations;

/// <summary>
/// Entity Framework Konfiguration für die Product-Entität
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Produkt-spezifische Eigenschaften
        builder.Property(p => p.Barcode)
            .HasMaxLength(50)
            .HasComment("Barcode/EAN");

        builder.Property(p => p.Manufacturer)
            .HasMaxLength(200)
            .HasComment("Hersteller");

        builder.Property(p => p.ManufacturerPartNumber)
            .HasMaxLength(100)
            .HasComment("Herstellerteilenummer");

        builder.Property(p => p.StorageLocation)
            .HasMaxLength(100)
            .HasComment("Lagerort");

        // Lagerbestand
        builder.Property(p => p.StockQuantity)
            .HasDefaultValue(0)
            .HasComment("Aktueller Lagerbestand");

        builder.Property(p => p.MinimumStock)
            .HasDefaultValue(0m)
            .HasComment("Mindestbestand");

        builder.Property(p => p.MaximumStock)
            .HasComment("Höchstbestand");

        // Physische Eigenschaften
        builder.Property(p => p.Weight)
            .HasPrecision(10, 3)
            .HasComment("Gewicht in kg");

        builder.Property(p => p.Dimensions)
            .HasMaxLength(100)
            .HasComment("Abmessungen (LxBxH)");

        // Zeiten - entfernt da nicht in Model vorhanden

        // Produkt-spezifische Indizes
        builder.HasIndex(p => p.Barcode)
            .IsUnique()
            .HasDatabaseName("IX_Products_Barcode")
            .HasFilter("[Barcode] IS NOT NULL AND [Barcode] != ''");

        builder.HasIndex(p => p.Manufacturer)
            .HasDatabaseName("IX_Products_Manufacturer");

        builder.HasIndex(p => p.SupplierId)
            .HasDatabaseName("IX_Products_SupplierId");

        builder.HasIndex(p => p.StockQuantity)
            .HasDatabaseName("IX_Products_StockQuantity");

        // Index für Warnung bei niedrigem Lagerbestand
        builder.HasIndex(p => new { p.StockQuantity, p.MinimumStock })
            .HasDatabaseName("IX_Products_LowStock");
    }
}
