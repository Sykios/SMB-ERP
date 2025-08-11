using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBErp.Domain.Customers;
using SMBErp.Domain.Inventory;
using SMBErp.Domain.Sales;
using SMBErp.Domain.Common;

namespace SMBErp.Infrastructure.Data.Configurations;

/// <summary>
/// Index-Optimierungen für performante Datenbankabfragen
/// </summary>
public static class IndexOptimizations
{
    /// <summary>
    /// Konfiguriert Performance-kritische Indizes für alle Entitäten
    /// </summary>
    public static void ConfigurePerformanceIndexes(this ModelBuilder modelBuilder)
    {
        ConfigureCustomerIndexes(modelBuilder);
        ConfigureItemIndexes(modelBuilder);
        ConfigureInvoiceIndexes(modelBuilder);
        ConfigureInvoiceItemIndexes(modelBuilder);
        ConfigureEmailTemplateIndexes(modelBuilder);
    }

    /// <summary>
    /// Customer-spezifische Index-Optimierungen
    /// </summary>
    private static void ConfigureCustomerIndexes(ModelBuilder modelBuilder)
    {
        var customerEntity = modelBuilder.Entity<Customer>();

        // Composite Index für häufige Suchkombinationen (CompanyName)
        customerEntity.HasIndex(c => c.CompanyName)
            .HasDatabaseName("IX_Customers_CompanyName")
            .HasFilter("[DeletedAt] IS NULL");

        // Index für E-Mail-basierte Suchen
        customerEntity.HasIndex(c => c.Email)
            .HasDatabaseName("IX_Customers_Email")
            .IsUnique()
            .HasFilter("[DeletedAt] IS NULL AND [Email] IS NOT NULL");

        // Composite Index für Kontaktpersonen
        customerEntity.HasIndex(c => new { c.ContactFirstName, c.ContactLastName })
            .HasDatabaseName("IX_Customers_ContactName")
            .HasFilter("[DeletedAt] IS NULL");

        // Index für Steuer-ID (Geschäftskunden)
        customerEntity.HasIndex(c => c.VatId)
            .HasDatabaseName("IX_Customers_VatId")
            .IsUnique()
            .HasFilter("[DeletedAt] IS NULL AND [VatId] IS NOT NULL");
    }

    /// <summary>
    /// Item/Product/Service-spezifische Index-Optimierungen
    /// </summary>
    private static void ConfigureItemIndexes(ModelBuilder modelBuilder)
    {
        var itemEntity = modelBuilder.Entity<Item>();

        // Composite Index für Artikel-Suche
        itemEntity.HasIndex(i => new { i.Category, i.Name })
            .HasDatabaseName("IX_Items_Category_Name")
            .HasFilter("[DeletedAt] IS NULL");

        // Index für Preisbereiche
        itemEntity.HasIndex(i => i.SalePrice)
            .HasDatabaseName("IX_Items_SalePrice")
            .HasFilter("[DeletedAt] IS NULL");

        // Composite Index für Sortierung und Filterung
        itemEntity.HasIndex(i => new { i.Category, i.SalePrice, i.CreatedAt })
            .HasDatabaseName("IX_Items_Category_Price_Created")
            .HasFilter("[DeletedAt] IS NULL");

        // Product-spezifische Indizes
        var productEntity = modelBuilder.Entity<Product>();
        
        productEntity.HasIndex(p => p.Barcode)
            .IsUnique()
            .HasDatabaseName("IX_Products_Barcode")
            .HasFilter("[Barcode] IS NOT NULL AND [DeletedAt] IS NULL");

        productEntity.HasIndex(p => p.StockQuantity)
            .HasDatabaseName("IX_Products_StockQuantity")
            .HasFilter("[DeletedAt] IS NULL");

        productEntity.HasIndex(p => new { p.ManufacturerPartNumber, p.Manufacturer })
            .HasDatabaseName("IX_Products_ManufacturerPart")
            .HasFilter("[DeletedAt] IS NULL AND [ManufacturerPartNumber] IS NOT NULL");

        // Service-spezifische Indizes
        var serviceEntity = modelBuilder.Entity<Service>();
        
        serviceEntity.HasIndex(s => s.IsRecurring)
            .HasDatabaseName("IX_Services_IsRecurring")
            .HasFilter("[DeletedAt] IS NULL");
    }

    /// <summary>
    /// Invoice-spezifische Index-Optimierungen
    /// </summary>
    private static void ConfigureInvoiceIndexes(ModelBuilder modelBuilder)
    {
        var invoiceEntity = modelBuilder.Entity<Invoice>();

        // Composite Index für Rechnungsübersicht
        invoiceEntity.HasIndex(i => new { i.CustomerId, i.Status, i.InvoiceDate })
            .HasDatabaseName("IX_Invoices_Customer_Status_Date")
            .HasFilter("[DeletedAt] IS NULL");

        // Index für Fälligkeitsdatum (für Mahnwesen)
        invoiceEntity.HasIndex(i => i.DueDate)
            .HasDatabaseName("IX_Invoices_DueDate")
            .HasFilter("[DeletedAt] IS NULL AND [Status] != 'Paid'");

        // Index für Rechnungsstatus
        invoiceEntity.HasIndex(i => i.Status)
            .HasDatabaseName("IX_Invoices_Status")
            .HasFilter("[DeletedAt] IS NULL");

        // Composite Index für Berichtswesen
        invoiceEntity.HasIndex(i => new { i.InvoiceDate, i.TotalAmount })
            .HasDatabaseName("IX_Invoices_Date_Amount")
            .HasFilter("[DeletedAt] IS NULL");

        // Index für Rechnungsnummer (eindeutig)
        invoiceEntity.HasIndex(i => i.InvoiceNumber)
            .IsUnique()
            .HasDatabaseName("IX_Invoices_InvoiceNumber")
            .HasFilter("[DeletedAt] IS NULL");
    }

    /// <summary>
    /// InvoiceItem-spezifische Index-Optimierungen
    /// </summary>
    private static void ConfigureInvoiceItemIndexes(ModelBuilder modelBuilder)
    {
        var invoiceItemEntity = modelBuilder.Entity<InvoiceItem>();

        // Composite Index für Rechnungsposition-Abfragen
        invoiceItemEntity.HasIndex(ii => new { ii.InvoiceId, ii.ItemId })
            .HasDatabaseName("IX_InvoiceItems_Invoice_Item");

        // Index für Artikel-bezogene Auswertungen
        invoiceItemEntity.HasIndex(ii => ii.ItemId)
            .HasDatabaseName("IX_InvoiceItems_ItemId");

        // Index für Sortierung nach Position
        invoiceItemEntity.HasIndex(ii => new { ii.InvoiceId, ii.Position })
            .HasDatabaseName("IX_InvoiceItems_Invoice_Position");
    }

    /// <summary>
    /// EmailTemplate-spezifische Index-Optimierungen
    /// </summary>
    private static void ConfigureEmailTemplateIndexes(ModelBuilder modelBuilder)
    {
        var emailTemplateEntity = modelBuilder.Entity<EmailTemplate>();

        // Index für Template-Typ und Sprache
        emailTemplateEntity.HasIndex(et => new { et.Type, et.Language })
            .HasDatabaseName("IX_EmailTemplates_Type_Language")
            .IsUnique()
            .HasFilter("[DeletedAt] IS NULL");

        // Index für aktive Templates
        emailTemplateEntity.HasIndex(et => et.IsActive)
            .HasDatabaseName("IX_EmailTemplates_IsActive")
            .HasFilter("[DeletedAt] IS NULL");
    }
}
