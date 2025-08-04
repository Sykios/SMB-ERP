/* DEAKTIVIERT FÜR PHASE 2.1 - WIRD SPÄTER REPARIERT
using Microsoft.EntityFrameworkCore;
using SMBErp.Models;
using SMBErp.Models.Enums;

namespace SMBErp.Data.Extensions;

/// <summary>
/// Extension-Methoden für das Seeding von Entwicklungsdaten
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Seed-Daten für die Entwicklungsumgebung
    /// </summary>
    public static void SeedDevelopmentData(this ModelBuilder modelBuilder)
    {
        SeedCompanyData(modelBuilder);
        SeedEmailTemplates(modelBuilder);
        SeedCustomers(modelBuilder);
        SeedItems(modelBuilder);
    }

    /// <summary>
    /// Seed Standard-Firmendaten
    /// </summary>
    private static void SeedCompanyData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                Id = 1,
                Name = "Musterfirma GmbH",
                LegalForm = "GmbH",
                Industry = "Dienstleistung",
                Street = "Musterstraße 123",
                City = "München",
                PostalCode = "80331",
                Country = "Deutschland",
                Phone = "+49 89 123456789",
                Email = "info@musterfirma.de",
                Website = "https://www.musterfirma.de",
                VatId = "DE123456789",
                TaxNumber = "123/456/78910",
                CommercialRegister = "München HRB",
                CommercialRegisterNumber = "123456",
                ManagingDirector = "Max Mustermann",
                DefaultPaymentTermDays = 30,
                DefaultVatRate = 19.00m,
                SmallBusinessRegulation = false,
                CreatedAt = DateTime.UtcNow
            }
        );
    }

    /// <summary>
    /// Seed Standard-E-Mail-Vorlagen
    /// </summary>
    private static void SeedEmailTemplates(ModelBuilder modelBuilder)
    {
        var templates = new List<EmailTemplate>
        {
            new EmailTemplate
            {
                Id = 1,
                Name = "Standard Rechnung",
                Description = "Standard-Vorlage für Rechnungsversand",
                Subject = "Ihre Rechnung {InvoiceNumber} vom {InvoiceDate:dd.MM.yyyy}",
                HtmlContent = @"
                    <p>Sehr geehrte Damen und Herren,</p>
                    <p>anbei erhalten Sie Ihre Rechnung {InvoiceNumber} vom {InvoiceDate:dd.MM.yyyy} über {TotalAmount:C}.</p>
                    <p>Die Rechnung ist bis zum {DueDate:dd.MM.yyyy} zur Zahlung fällig.</p>
                    <p>Bei Fragen stehen wir Ihnen gerne zur Verfügung.</p>
                    <p>Mit freundlichen Grüßen<br/>{CompanyName}</p>",
                TextContent = @"
                    Sehr geehrte Damen und Herren,

                    anbei erhalten Sie Ihre Rechnung {InvoiceNumber} vom {InvoiceDate:dd.MM.yyyy} über {TotalAmount:C}.

                    Die Rechnung ist bis zum {DueDate:dd.MM.yyyy} zur Zahlung fällig.

                    Bei Fragen stehen wir Ihnen gerne zur Verfügung.

                    Mit freundlichen Grüßen
                    {CompanyName}",
                TemplateType = EmailTemplateType.Invoice,
                Language = "de-DE",
                IsDefault = true,
                IsActive = true,
                IsSystemTemplate = true,
                AttachInvoicePdf = true,
                BccToSender = true,
                CreatedAt = DateTime.UtcNow
            },
            new EmailTemplate
            {
                Id = 2,
                Name = "Zahlungserinnerung",
                Description = "Freundliche Zahlungserinnerung",
                Subject = "Zahlungserinnerung - Rechnung {InvoiceNumber}",
                HtmlContent = @"
                    <p>Sehr geehrte Damen und Herren,</p>
                    <p>wir möchten Sie daran erinnern, dass die Rechnung {InvoiceNumber} vom {InvoiceDate:dd.MM.yyyy} 
                    über {TotalAmount:C} noch nicht beglichen wurde.</p>
                    <p>Das Zahlungsziel war der {DueDate:dd.MM.yyyy}.</p>
                    <p>Falls Sie die Zahlung bereits veranlasst haben, betrachten Sie diese E-Mail als gegenstandslos.</p>
                    <p>Mit freundlichen Grüßen<br/>{CompanyName}</p>",
                TemplateType = EmailTemplateType.PaymentReminder,
                Language = "de-DE",
                IsDefault = true,
                IsActive = true,
                IsSystemTemplate = true,
                AttachInvoicePdf = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        modelBuilder.Entity<EmailTemplate>().HasData(templates);
    }

    /// <summary>
    /// Seed Beispiel-Kunden
    /// </summary>
    private static void SeedCustomers(ModelBuilder modelBuilder)
    {
        var customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                CustomerNumber = "K-2025-001",
                CompanyName = "Musterkunde GmbH",
                FirstName = "Anna",
                LastName = "Muster",
                Email = "anna.muster@musterkunde.de",
                Phone = "+49 89 987654321",
                Street = "Kundenstraße 456",
                City = "Berlin",
                PostalCode = "10115",
                Country = "Deutschland",
                CustomerType = CustomerType.BusinessCustomer,
                Status = CustomerStatus.Active,
                PaymentTermDays = 30,
                CreatedAt = DateTime.UtcNow
            },
            new Customer
            {
                Id = 2,
                CustomerNumber = "K-2025-002",
                FirstName = "Peter",
                LastName = "Privatmann",
                Email = "peter.privatmann@email.de",
                Phone = "+49 30 123456789",
                Street = "Privatstraße 789",
                City = "Hamburg",
                PostalCode = "20095",
                Country = "Deutschland",
                CustomerType = CustomerType.PrivateCustomer,
                Status = CustomerStatus.Active,
                PaymentTermDays = 14,
                CreatedAt = DateTime.UtcNow
            }
        };

        modelBuilder.Entity<Customer>().HasData(customers);
    }

    /// <summary>
    /// Seed Beispiel-Artikel (Produkte und Dienstleistungen)
    /// </summary>
    private static void SeedItems(ModelBuilder modelBuilder)
    {
        // Beispiel-Produkte
        var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                ItemNumber = "P-2025-001",
                Name = "Beispiel Produkt 1",
                Description = "Dies ist ein Beispielprodukt für Testzwecke",
                Category = "Hardware",
                Unit = "Stk",
                SalePrice = 99.99m,
                PurchasePrice = 59.99m,
                VatRate = 19.00m,
                Status = ItemStatus.Active,
                StockQuantity = 50,
                MinimumStock = 10,
                Barcode = "1234567890123",
                Manufacturer = "Beispiel AG",
                Weight = 1.5m,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = 2,
                ItemNumber = "P-2025-002",
                Name = "Software Lizenz",
                Description = "Jahreslizenz für Business Software",
                Category = "Software",
                Unit = "Lizenz",
                SalePrice = 299.00m,
                PurchasePrice = 199.00m,
                VatRate = 19.00m,
                Status = ItemStatus.Active,
                StockQuantity = 0, // Digitales Produkt
                MinimumStock = 0,
                CreatedAt = DateTime.UtcNow
            }
        };

        // Beispiel-Dienstleistungen
        var services = new List<Service>
        {
            new Service
            {
                Id = 3,
                ItemNumber = "S-2025-001",
                Name = "IT-Beratung",
                Description = "Professionelle IT-Beratung pro Stunde",
                Category = "Beratung",
                Unit = "Stunde",
                SalePrice = 120.00m,
                VatRate = 19.00m,
                Status = ItemStatus.Active,
                EstimatedDurationHours = 1.0m,
                BillingRhythm = BillingRhythm.OneTime,
                IsRecurring = false,
                RequiresAppointment = true,
                CanBePerformedRemotely = true,
                CreatedAt = DateTime.UtcNow
            },
            new Service
            {
                Id = 4,
                ItemNumber = "S-2025-002",
                Name = "Wartungsvertrag",
                Description = "Monatlicher Wartungsvertrag für IT-Infrastruktur",
                Category = "Wartung",
                Unit = "Monat",
                SalePrice = 299.00m,
                VatRate = 19.00m,
                Status = ItemStatus.Active,
                EstimatedDurationHours = 4.0m,
                BillingRhythm = BillingRhythm.Monthly,
                IsRecurring = true,
                RequiresAppointment = false,
                CanBePerformedRemotely = true,
                TravelCostPerKm = 0.30m,
                CreatedAt = DateTime.UtcNow
            }
        };

        modelBuilder.Entity<Product>().HasData(products);
        modelBuilder.Entity<Service>().HasData(services);
    }
}
*/
