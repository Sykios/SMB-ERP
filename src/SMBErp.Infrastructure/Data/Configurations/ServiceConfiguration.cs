using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBErp.Domain.Inventory;

namespace SMBErp.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Konfiguration für die Service-Entität
/// </summary>
public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        // Service-spezifische Eigenschaften
        builder.Property(s => s.EstimatedDurationHours)
            .HasPrecision(10, 2)
            .HasComment("Geschätzte Dauer in Stunden");

        builder.Property(s => s.WorkLocation)
            .HasMaxLength(200)
            .HasComment("Arbeitsort/Standort");

        builder.Property(s => s.AdditionalMaterialCosts)
            .HasPrecision(18, 2)
            .HasDefaultValue(0m)
            .HasComment("Materialkosten");

        builder.Property(s => s.TravelCostPerKm)
            .HasPrecision(10, 2)
            .HasDefaultValue(0m)
            .HasComment("Reisekosten pro Kilometer");

        builder.Property(s => s.FlatTravelCost)
            .HasPrecision(18, 2)
            .HasDefaultValue(0m)
            .HasComment("Pauschale Reisekosten");

        // String-Eigenschaften
        builder.Property(s => s.BillingRhythm)
            .HasMaxLength(50)
            .HasComment("Abrechnungsrhythmus");

        // Boolean-Eigenschaften
        builder.Property(s => s.IsRecurring)
            .HasDefaultValue(false)
            .HasComment("Wiederkehrender Service");

        builder.Property(s => s.CanBeRemote)
            .HasDefaultValue(false)
            .HasComment("Remote durchführbar");

        // Service-spezifische Indizes
        builder.HasIndex(s => s.BillingRhythm)
            .HasDatabaseName("IX_Services_BillingRhythm");

        builder.HasIndex(s => s.IsRecurring)
            .HasDatabaseName("IX_Services_IsRecurring");

        builder.HasIndex(s => s.EstimatedDurationHours)
            .HasDatabaseName("IX_Services_Duration");
    }
}
