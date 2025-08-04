using Microsoft.EntityFrameworkCore;
using SMBErp.Models;
using SMBErp.Models.Enums;

namespace SMBErp.Data.Extensions;

/// <summary>
/// Extension-Methoden für das Seeding von realistischen deutschen Entwicklungsdaten
/// </summary>
public static class SeedDataExtensions
{
    /// <summary>
    /// Seed-Daten für die Entwicklungsumgebung
    /// </summary>
    public static void SeedDevelopmentData(this ModelBuilder modelBuilder)
    {
        SeedCompanyData(modelBuilder);
        SeedEmailTemplates(modelBuilder);
        SeedCustomers(modelBuilder);
        SeedProducts(modelBuilder);
        SeedServices(modelBuilder);
    }

    /// <summary>
    /// Seed Standard-Firmendaten (Deutsche Muster-Firma)
    /// </summary>
    private static void SeedCompanyData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                Id = 1,
                Name = "Mustermann Consulting GmbH",
                LegalForm = "GmbH",
                Street = "Musterstraße 123",
                City = "München",
                ZipCode = "80331",
                Country = "Deutschland",
                Phone = "+49 89 12345678",
                Fax = "+49 89 12345679",
                Email = "info@mustermann-consulting.de",
                Website = "https://www.mustermann-consulting.de",
                TaxNumber = "143/815/08154",
                VatId = "DE123456789",
                IBAN = "DE89 3704 0044 0532 0130 00",
                BIC = "COBADEFFXXX",
                DefaultPaymentTermDays = 30,
                DefaultVatRate = 19.0m,
                CreatedAt = DateTime.UtcNow.AddDays(-365),
                UpdatedAt = DateTime.UtcNow.AddDays(-30)
            }
        );
    }

    /// <summary>
    /// Seed E-Mail-Vorlagen
    /// </summary>
    private static void SeedEmailTemplates(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailTemplate>().HasData(
            new EmailTemplate
            {
                Id = 1,
                Name = "Rechnung versenden",
                Subject = "Ihre Rechnung {InvoiceNumber} von {CompanyName}",
                Body = @"Sehr geehrte Damen und Herren,

anbei erhalten Sie Ihre Rechnung {InvoiceNumber} vom {InvoiceDate} über {TotalAmount} EUR.

Die Rechnung ist zahlbar bis zum {DueDate} ohne Abzug.

Bei Fragen stehen wir Ihnen gerne zur Verfügung.

Mit freundlichen Grüßen
{CompanyName}

{CompanyStreet}
{CompanyZipCode} {CompanyCity}
Tel: {CompanyPhone}
E-Mail: {CompanyEmail}",
                Type = EmailTemplateType.Invoice,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-365),
                UpdatedAt = DateTime.UtcNow.AddDays(-365)
            },
            new EmailTemplate
            {
                Id = 2,
                Name = "Zahlungserinnerung",
                Subject = "Freundliche Zahlungserinnerung - Rechnung {InvoiceNumber}",
                Body = @"Sehr geehrte Damen und Herren,

wir möchten Sie freundlich daran erinnern, dass die Rechnung {InvoiceNumber} vom {InvoiceDate} über {TotalAmount} EUR noch nicht beglichen wurde.

Das Zahlungsziel war der {DueDate}.

Falls Sie die Zahlung bereits veranlasst haben, betrachten Sie dieses Schreiben als gegenstandslos.

Mit freundlichen Grüßen
{CompanyName}",
                Type = EmailTemplateType.PaymentReminder,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-365),
                UpdatedAt = DateTime.UtcNow.AddDays(-365)
            }
        );
    }

    /// <summary>
    /// Seed Testkunden (realistische deutsche Unternehmen)
    /// </summary>
    private static void SeedCustomers(ModelBuilder modelBuilder)
    {
        var customers = new[]
        {
            new Customer
            {
                Id = 1,
                CustomerNumber = "K-2024-001",
                CompanyName = "TechnoSoft AG",
                ContactFirstName = "Dr. Michael",
                ContactLastName = "Schmidt",
                Email = "m.schmidt@technosoft.de",
                Phone = "+49 40 123456789",
                BillingStreet = "Hamburger Allee 45",
                BillingCity = "Hamburg",
                BillingZipCode = "20095",
                BillingCountry = "Deutschland",
                ShippingStreet = "Hamburger Allee 45",
                ShippingCity = "Hamburg",
                ShippingZipCode = "20095",
                ShippingCountry = "Deutschland",
                VatId = "DE234567890",
                PaymentTermDays = 30,
                CreatedAt = DateTime.UtcNow.AddDays(-180),
                UpdatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new Customer
            {
                Id = 2,
                CustomerNumber = "K-2024-002",
                CompanyName = "Bäckerei Müller e.K.",
                ContactFirstName = "Anna",
                ContactLastName = "Müller",
                Email = "info@baeckerei-mueller.de",
                Phone = "+49 89 987654321",
                BillingStreet = "Marienplatz 12",
                BillingCity = "München",
                BillingZipCode = "80331",
                BillingCountry = "Deutschland",
                ShippingStreet = "Marienplatz 12",
                ShippingCity = "München",
                ShippingZipCode = "80331",
                ShippingCountry = "Deutschland",
                VatId = "DE345678901",
                PaymentTermDays = 14,
                CreatedAt = DateTime.UtcNow.AddDays(-120),
                UpdatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Customer
            {
                Id = 3,
                CustomerNumber = "K-2024-003",
                CompanyName = "MaxMuster Einzelunternehmen",
                ContactFirstName = "Max",
                ContactLastName = "Mustermann",
                Email = "max@mustermann.de",
                Phone = "+49 30 111222333",
                BillingStreet = "Unter den Linden 77",
                BillingCity = "Berlin",
                BillingZipCode = "10117",
                BillingCountry = "Deutschland",
                ShippingStreet = "Unter den Linden 77",
                ShippingCity = "Berlin",
                ShippingZipCode = "10117",
                ShippingCountry = "Deutschland",
                VatId = null, // Kleinunternehmer
                PaymentTermDays = 7,
                CreatedAt = DateTime.UtcNow.AddDays(-60),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            }
        };

        modelBuilder.Entity<Customer>().HasData(customers);
    }

    /// <summary>
    /// Seed Beispielprodukte
    /// </summary>
    private static void SeedProducts(ModelBuilder modelBuilder)
    {
        var products = new[]
        {
            new Product
            {
                Id = 1,
                ItemNumber = "P-SW-001",
                Name = "Business Software Lizenz",
                Description = "Jahres-Lizenz für Business Management Software",
                Category = "Software",
                SalePrice = 1200.00m,
                PurchasePrice = 600.00m,
                Unit = Unit.Piece,
                VatRate = 19.0m,
                StockQuantity = 50,
                MinimumStock = 10,
                MaximumStock = 100,
                Barcode = "4012345678901",
                Weight = 0.1m,
                Manufacturer = "SoftwareTech GmbH",
                SupplierId = null,
                CreatedAt = DateTime.UtcNow.AddDays(-200),
                UpdatedAt = DateTime.UtcNow.AddDays(-50)
            },
            new Product
            {
                Id = 2,
                ItemNumber = "P-HW-002",
                Name = "Wireless Maus",
                Description = "Ergonomische kabellose Maus mit USB-Receiver",
                Category = "Hardware",
                SalePrice = 45.00m,
                PurchasePrice = 25.00m,
                Unit = Unit.Piece,
                VatRate = 19.0m,
                StockQuantity = 25,
                MinimumStock = 5,
                MaximumStock = 50,
                Barcode = "4012345678902",
                Weight = 0.2m,
                Manufacturer = "TechPeripheral AG",
                SupplierId = null,
                CreatedAt = DateTime.UtcNow.AddDays(-150),
                UpdatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new Product
            {
                Id = 3,
                ItemNumber = "P-BK-003",
                Name = "Fachbuch: Digitalisierung im Mittelstand",
                Description = "Praxishandbuch für digitale Transformation",
                Category = "Bücher",
                SalePrice = 39.90m,
                PurchasePrice = 20.00m,
                Unit = Unit.Piece,
                VatRate = 7.0m, // Ermäßigter Steuersatz für Bücher
                StockQuantity = 15,
                MinimumStock = 3,
                MaximumStock = 30,
                Barcode = "9783123456789",
                Weight = 0.8m,
                Manufacturer = "Business Verlag GmbH",
                SupplierId = null,
                CreatedAt = DateTime.UtcNow.AddDays(-100),
                UpdatedAt = DateTime.UtcNow.AddDays(-10)
            }
        };

        modelBuilder.Entity<Product>().HasData(products);
    }

    /// <summary>
    /// Seed Beispieldienstleistungen
    /// </summary>
    private static void SeedServices(ModelBuilder modelBuilder)
    {
        var services = new[]
        {
            new Service
            {
                Id = 4,
                ItemNumber = "S-CON-001",
                Name = "IT-Beratung",
                Description = "Professionelle IT-Beratung und -Konzeption",
                Category = "Beratung",
                SalePrice = 120.00m,
                PurchasePrice = 0.00m,
                Unit = Unit.Hour,
                VatRate = 19.0m,
                EstimatedDurationHours = 1.0m,
                BillingRhythm = "Stündlich",
                IsRecurring = false,
                WorkLocation = "Vor Ort beim Kunden",
                CreatedAt = DateTime.UtcNow.AddDays(-365),
                UpdatedAt = DateTime.UtcNow.AddDays(-100)
            },
            new Service
            {
                Id = 5,
                ItemNumber = "S-SUP-002",
                Name = "Software Support",
                Description = "Technischer Support und Wartung für Business Software",
                Category = "Support",
                SalePrice = 89.00m,
                PurchasePrice = 0.00m,
                Unit = Unit.Hour,
                VatRate = 19.0m,
                EstimatedDurationHours = 1.0m,
                BillingRhythm = "Monatlich",
                IsRecurring = true,
                WorkLocation = "Remote",
                CreatedAt = DateTime.UtcNow.AddDays(-300),
                UpdatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new Service
            {
                Id = 6,
                ItemNumber = "S-TRA-003",
                Name = "Mitarbeiterschulung",
                Description = "Schulung der Mitarbeiter in neuen Software-Systemen",
                Category = "Schulung",
                SalePrice = 450.00m,
                PurchasePrice = 0.00m,
                Unit = Unit.Day,
                VatRate = 19.0m,
                EstimatedDurationHours = 8.0m,
                BillingRhythm = "Einmalig",
                IsRecurring = false,
                WorkLocation = "Vor Ort beim Kunden",
                CreatedAt = DateTime.UtcNow.AddDays(-200),
                UpdatedAt = DateTime.UtcNow.AddDays(-60)
            }
        };

        modelBuilder.Entity<Service>().HasData(services);
    }
}
