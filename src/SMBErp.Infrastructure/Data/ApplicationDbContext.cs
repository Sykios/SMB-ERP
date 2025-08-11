using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMBErp.Domain.Customers;
using SMBErp.Domain.Sales;
using SMBErp.Domain.Inventory;
using SMBErp.Domain.Company;
using SMBErp.Domain.Common;
using SMBErp.Infrastructure.Data.Configurations;
using System.Linq.Expressions;
using static SMBErp.Infrastructure.Data.Configurations.IndexOptimizations;

namespace SMBErp.Infrastructure.Data;

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

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity Configurations anwenden
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new ItemConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceItemConfiguration());
        modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());

        // Performance-kritische Indizes konfigurieren
        modelBuilder.ConfigurePerformanceIndexes();
    }
}
