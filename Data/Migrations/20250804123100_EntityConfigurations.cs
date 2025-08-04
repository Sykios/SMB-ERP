using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMBErp.Data.Migrations
{
    /// <inheritdoc />
    public partial class EntityConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "Firmenname"),
                    LegalForm = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "Rechtsform (GmbH, AG, etc.)"),
                    ManagingDirector = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Street = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "Straße und Hausnummer"),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, comment: "Postleitzahl"),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "Stadt"),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "Land"),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "Telefonnummer"),
                    Fax = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "Faxnummer"),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false, comment: "E-Mail-Adresse"),
                    Website = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, comment: "Website URL"),
                    VatId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "Umsatzsteuer-Identifikationsnummer"),
                    TaxNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "Steuernummer"),
                    CommercialRegister = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Court = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    BankName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IBAN = table.Column<string>(type: "TEXT", maxLength: 34, nullable: true, comment: "IBAN Bankkonto"),
                    BIC = table.Column<string>(type: "TEXT", maxLength: 11, nullable: true, comment: "BIC/SWIFT Code"),
                    AccountHolder = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    BankName2 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IBAN2 = table.Column<string>(type: "TEXT", maxLength: 34, nullable: true),
                    BIC2 = table.Column<string>(type: "TEXT", maxLength: 11, nullable: true),
                    LogoPath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CompanyColor = table.Column<string>(type: "TEXT", maxLength: 7, nullable: true),
                    SecondaryColor = table.Column<string>(type: "TEXT", maxLength: 7, nullable: true),
                    DefaultPaymentTermDays = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 30, comment: "Standard Zahlungsziel in Tagen"),
                    DefaultVatRate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, defaultValue: 19.00m, comment: "Standard Mehrwertsteuersatz in Prozent"),
                    Currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    AdditionalInfo = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    IsSmallBusiness = table.Column<bool>(type: "INTEGER", nullable: false),
                    SmallBusinessText = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ContactFirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContactLastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    AlternativeEmail = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    BillingStreet = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    BillingZipCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    BillingCity = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BillingCountry = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, defaultValue: "Österreich"),
                    ShippingStreet = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ShippingZipCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    ShippingCity = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ShippingCountry = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    VatId = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    TaxNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    PaymentTermDays = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 14),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    DiscountDays = table.Column<int>(type: "INTEGER", nullable: true),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Active"),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastContactDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Body = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    IsHtml = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    Language = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AvailablePlaceholders = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    AttachPdfAutomatically = table.Column<bool>(type: "INTEGER", nullable: false),
                    BccEmail = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ReplyToEmail = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Priority = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "Eindeutige Artikelnummer"),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "Artikelname"),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, comment: "Artikelbeschreibung"),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Verkaufspreis (netto)"),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, comment: "Einkaufspreis (netto)"),
                    VatRate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, defaultValue: 19.00m, comment: "Mehrwertsteuersatz in Prozent"),
                    Unit = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1, comment: "Mengeneinheit"),
                    Category = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "Artikelkategorie"),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Erstellungsdatum"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "Letzte Änderung"),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ItemType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Barcode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "Barcode/EAN"),
                    StockQuantity = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 2, nullable: true, defaultValue: 0m, comment: "Aktueller Lagerbestand"),
                    MinimumStock = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 2, nullable: true, defaultValue: 0m, comment: "Mindestbestand"),
                    MaximumStock = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 2, nullable: true, comment: "Höchstbestand"),
                    StorageLocation = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "Lagerort"),
                    Weight = table.Column<decimal>(type: "decimal(18,3)", precision: 10, scale: 3, nullable: true, comment: "Gewicht in kg"),
                    Dimensions = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "Abmessungen (LxBxH)"),
                    Manufacturer = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true, comment: "Hersteller"),
                    ManufacturerPartNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "Herstellerteilenummer"),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: true),
                    EstimatedDurationHours = table.Column<decimal>(type: "decimal(8,2)", precision: 10, scale: 2, nullable: true, comment: "Geschätzte Dauer in Stunden"),
                    MinimumDurationHours = table.Column<decimal>(type: "decimal(8,2)", precision: 18, scale: 2, nullable: true),
                    BillingRhythm = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "Abrechnungsrhythmus"),
                    IsRecurring = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: false, comment: "Wiederkehrender Service"),
                    RecurrenceInterval = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    RequiredQualifications = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    WorkLocation = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true, comment: "Arbeitsort/Standort"),
                    CanBeRemote = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: false, comment: "Remote durchführbar"),
                    AdditionalMaterialCosts = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m, comment: "Materialkosten"),
                    TravelCostPerKm = table.Column<decimal>(type: "decimal(8,2)", precision: 10, scale: 2, nullable: true, defaultValue: 0m, comment: "Reisekosten pro Kilometer"),
                    FlatTravelCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, defaultValue: 0m, comment: "Pauschale Reisekosten")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Draft"),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    DiscountDays = table.Column<int>(type: "INTEGER", nullable: true),
                    PaymentTermDays = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 14),
                    Subject = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IntroductionText = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ConclusionText = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    InternalNotes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CustomerReference = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ProjectNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PaidDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false, comment: "Rechnungs-ID"),
                    Position = table.Column<int>(type: "INTEGER", nullable: false, comment: "Positionsnummer in der Rechnung"),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: true, comment: "Artikel-ID (optional)"),
                    ItemNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false, comment: "Positionsbeschreibung"),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 4, nullable: false, comment: "Menge"),
                    Unit = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1, comment: "Mengeneinheit"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Einzelpreis (netto)"),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    VatRate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, comment: "Mehrwertsteuersatz in Prozent"),
                    AdditionalInfo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoice",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Item",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Email",
                table: "Companies",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_TaxNumber",
                table: "Companies",
                column: "TaxNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_VatId",
                table: "Companies",
                column: "VatId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ContactName",
                table: "Customers",
                columns: new[] { "ContactLastName", "ContactFirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedAt",
                table: "Customers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerNumber",
                table: "Customers",
                column: "CustomerNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Status",
                table: "Customers",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_IsActive",
                table: "EmailTemplates",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_Name",
                table: "EmailTemplates",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_Type",
                table: "EmailTemplates",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_Invoice_Position",
                table: "InvoiceItems",
                columns: new[] { "InvoiceId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ItemId",
                table: "InvoiceItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_UnitPrice",
                table: "InvoiceItems",
                column: "UnitPrice");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_VatRate",
                table: "InvoiceItems",
                column: "VatRate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DueDate",
                table: "Invoices",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceDate",
                table: "Invoices",
                column: "InvoiceDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Status",
                table: "Invoices",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Category",
                table: "Items",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedAt",
                table: "Items",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemNumber",
                table: "Items",
                column: "ItemNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SalePrice",
                table: "Items",
                column: "SalePrice");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Barcode",
                table: "Items",
                column: "Barcode",
                unique: true,
                filter: "[Barcode] IS NOT NULL AND [Barcode] != ''");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LowStock",
                table: "Items",
                columns: new[] { "StockQuantity", "MinimumStock" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Manufacturer",
                table: "Items",
                column: "Manufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockQuantity",
                table: "Items",
                column: "StockQuantity");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Items",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_BillingRhythm",
                table: "Items",
                column: "BillingRhythm");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Duration",
                table: "Items",
                column: "EstimatedDurationHours");

            migrationBuilder.CreateIndex(
                name: "IX_Services_IsRecurring",
                table: "Items",
                column: "IsRecurring");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
