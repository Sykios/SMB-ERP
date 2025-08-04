using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMBErp.Data.Configurations;

/// <summary>
/// Basisklasse für Entity Configurations mit gemeinsamen Methoden
/// </summary>
public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public abstract void Configure(EntityTypeBuilder<T> builder);

    /// <summary>
    /// Konfiguriert Audit-Eigenschaften (CreatedAt, UpdatedAt)
    /// </summary>
    protected void ConfigureAuditProperties(EntityTypeBuilder<T> builder)
    {
        // Diese Methode kann von abgeleiteten Klassen verwendet werden
        // um einheitliche Audit-Eigenschaften zu konfigurieren
        
        // Prüfe ob die Entität CreatedAt Eigenschaft hat
        var createdAtProperty = typeof(T).GetProperty("CreatedAt");
        if (createdAtProperty != null)
        {
            builder.Property("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Erstellungsdatum");
        }

        // Prüfe ob die Entität UpdatedAt Eigenschaft hat
        var updatedAtProperty = typeof(T).GetProperty("UpdatedAt");
        if (updatedAtProperty != null)
        {
            builder.Property("UpdatedAt")
                .HasComment("Letzte Änderung");
        }
    }

    /// <summary>
    /// Konfiguriert einen Standard-Index für Created/Updated Timestamps
    /// </summary>
    protected void ConfigureTimestampIndexes(EntityTypeBuilder<T> builder, string tableName)
    {
        var createdAtProperty = typeof(T).GetProperty("CreatedAt");
        if (createdAtProperty != null)
        {
            builder.HasIndex("CreatedAt")
                .HasDatabaseName($"IX_{tableName}_CreatedAt");
        }

        var updatedAtProperty = typeof(T).GetProperty("UpdatedAt");
        if (updatedAtProperty != null)
        {
            builder.HasIndex("UpdatedAt")
                .HasDatabaseName($"IX_{tableName}_UpdatedAt");
        }
    }

    /// <summary>
    /// Konfiguriert Soft Delete Funktionalität
    /// </summary>
    protected void ConfigureSoftDelete(EntityTypeBuilder<T> builder)
    {
        var isDeletedProperty = typeof(T).GetProperty("IsDeleted");
        if (isDeletedProperty != null)
        {
            builder.Property("IsDeleted")
                .HasDefaultValue(false)
                .HasComment("Soft Delete Flag");

            // Global Query Filter für Soft Delete
            builder.HasQueryFilter(e => !EF.Property<bool>(e, "IsDeleted"));

            // Index für bessere Performance bei Soft Delete Queries
            builder.HasIndex("IsDeleted")
                .HasDatabaseName($"IX_{typeof(T).Name}_IsDeleted");
        }
    }

    /// <summary>
    /// Konfiguriert deutsche Dezimal-Präzision für Geldbeträge
    /// </summary>
    protected void ConfigureMoneyProperty(EntityTypeBuilder<T> builder, string propertyName, string comment = null)
    {
        builder.Property(propertyName)
            .HasPrecision(18, 2)
            .HasComment(comment ?? "Geldbetrag in Euro");
    }

    /// <summary>
    /// Konfiguriert Prozentsatz-Eigenschaft
    /// </summary>
    protected void ConfigurePercentageProperty(EntityTypeBuilder<T> builder, string propertyName, string comment = null)
    {
        builder.Property(propertyName)
            .HasPrecision(5, 2)
            .HasComment(comment ?? "Prozentsatz");
    }
}
