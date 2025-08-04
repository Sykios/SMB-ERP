using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMBErp.Models;
using SMBErp.Data.Configurations;

namespace SMBErp.Data;

/// <summary>
/// Haupt-Datenbankkontext für das SMB ERP System
/// </summary>
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    #region DbSets

    /// <summary>
    /// Kundenstammdaten
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Artikel (Produkte und Dienstleistungen)
    /// </summary>
    public DbSet<Item> Items { get; set; }

    /// <summary>
    /// Produkte (erbt von Item)
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Dienstleistungen (erbt von Item)
    /// </summary>
    public DbSet<Service> Services { get; set; }

    /// <summary>
    /// Rechnungen
    /// </summary>
    public DbSet<Invoice> Invoices { get; set; }

    /// <summary>
    /// Rechnungspositionen
    /// </summary>
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    /// <summary>
    /// Firmeneinstellungen
    /// </summary>
    public DbSet<Company> Company { get; set; }

    /// <summary>
    /// E-Mail-Vorlagen
    /// </summary>
    public DbSet<EmailTemplate> EmailTemplates { get; set; }

    #endregion

    #region OnModelCreating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity Configurations anwenden
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new ItemConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceItemConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());

        // Globale Konfigurationen
        ConfigureGlobalSettings(modelBuilder);

        // Seed-Daten werden später hinzugefügt
        // modelBuilder.SeedData(); // deaktiviert für jetzt
    }

    /// <summary>
    /// Globale Datenbankeinstellungen
    /// </summary>
    private void ConfigureGlobalSettings(ModelBuilder modelBuilder)
    {
        // Standardeinstellungen für alle String-Properties ohne explizite Länge
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(string) && property.GetMaxLength() == null)
                {
                    // Setze eine Standard-Maximallänge für Strings ohne explizite Länge
                    property.SetMaxLength(255);
                }

                // Konfiguriere Dezimal-Properties für deutsche Währung
                if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                {
                    var precision = property.GetPrecision();
                    var scale = property.GetScale();
                    
                    // Wenn keine Precision/Scale gesetzt ist, verwende Standard für Geld
                    if (!precision.HasValue && !scale.HasValue)
                    {
                        property.SetPrecision(18);
                        property.SetScale(2);
                    }
                }
            }
        }

        // SQLite-spezifische Konfigurationen
        ConfigureSqliteSpecific(modelBuilder);
    }

    /// <summary>
    /// SQLite-spezifische Konfigurationen
    /// </summary>
    private void ConfigureSqliteSpecific(ModelBuilder modelBuilder)
    {
        // SQLite unterstützt keine echten Dezimal-Typen, also verwenden wir TEXT mit CHECK Constraints
        // für kritische Geldbeträge (wird automatisch von EF Core gehandhabt)

        // Aktiviere Foreign Key Constraints in SQLite
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            // SQLite-spezifische Einstellungen können hier hinzugefügt werden
            // Foreign Keys sind standardmäßig in modernen SQLite-Versionen aktiviert
        }
    }

    #endregion

    #region SaveChanges Override

    /// <summary>
    /// Überschreibt SaveChanges um automatische Audit-Funktionalität hinzuzufügen
    /// </summary>
    public override int SaveChanges()
    {
        UpdateAuditProperties();
        return base.SaveChanges();
    }

    /// <summary>
    /// Überschreibt SaveChangesAsync um automatische Audit-Funktionalität hinzuzufügen
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Aktualisiert Audit-Eigenschaften automatisch
    /// </summary>
    private void UpdateAuditProperties()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        var currentTime = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            // UpdatedAt für alle Änderungen setzen
            if (entry.Entity.GetType().GetProperty("UpdatedAt") != null)
            {
                entry.Property("UpdatedAt").CurrentValue = currentTime;
            }

            // CreatedAt nur bei neuen Entitäten setzen
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.GetType().GetProperty("CreatedAt") != null)
                {
                    entry.Property("CreatedAt").CurrentValue = currentTime;
                }
            }
        }
    }

    #endregion
}
