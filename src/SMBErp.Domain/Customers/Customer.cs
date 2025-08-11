using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SMBErp.Domain.Common;
using SMBErp.Domain.Shared;

namespace SMBErp.Domain.Customers;

/// <summary>
/// Kundenstammdaten
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Kundennummer (eindeutig, automatisch generiert)
    /// </summary>
    [Required]
    [StringLength(20)]
    [Display(Name = "Kundennummer")]
    public required string CustomerNumber { get; set; }

    /// <summary>
    /// Firmenname (optional, falls B2B)
    /// </summary>
    [StringLength(200)]
    [Display(Name = "Firmenname")]
    public string? CompanyName { get; set; }

    /// <summary>
    /// Kontaktperson - Vorname
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "Vorname Kontaktperson")]
    public required string ContactFirstName { get; set; }

    /// <summary>
    /// Kontaktperson - Nachname
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "Nachname Kontaktperson")]
    public required string ContactLastName { get; set; }

    /// <summary>
    /// E-Mail-Adresse (Hauptkontakt) (optional, entweder EMail oder Telefon)
    /// </summary>
    [EmailAddress]
    [StringLength(200)]
    [Display(Name = "E-Mail")]
    public string? Email { get; set; }

    /// <summary>
    /// Alternative E-Mail-Adresse
    /// </summary>
    [EmailAddress]
    [StringLength(200)]
    [Display(Name = "Alternative E-Mail")]
    public string? AlternativeEmail { get; set; }

    /// <summary>
    /// Telefonnummer (optional, entweder EMail oder Telefon)
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Telefon")]
    public string? Phone { get; set; }

    /// <summary>
    /// Mobiltelefonnummer (optional, entweder EMail oder Telefon)
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Mobil")]
    public string? Mobile { get; set; }

    /// <summary>
    /// Faxnummer
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Fax")]
    public string? Fax { get; set; }

    /// <summary>
    /// Website/Homepage
    /// </summary>
    [Url]
    [StringLength(200)]
    [Display(Name = "Website")]
    public string? Website { get; set; }

    // Rechnungsadresse
    /// <summary>
    /// Straße und Hausnummer (Rechnungsadresse)
    /// </summary>
    [Required]
    [StringLength(200)]
    [Display(Name = "Straße")]
    public required string BillingStreet { get; set; }

    /// <summary>
    /// Postleitzahl (Rechnungsadresse)
    /// </summary>
    [Required]
    [StringLength(20)]
    [Display(Name = "PLZ")]
    public required string BillingZipCode { get; set; }

    /// <summary>
    /// Stadt (Rechnungsadresse)
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "Stadt")]
    public required string BillingCity { get; set; }

    /// <summary>
    /// Land (Rechnungsadresse)
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "Land")]
    public string BillingCountry { get; set; } = "Österreich";

    // Lieferadresse (optional, falls abweichend)
    /// <summary>
    /// Straße und Hausnummer (Lieferadresse)
    /// </summary>
    [StringLength(200)]
    [Display(Name = "Lieferstraße")]
    public string? ShippingStreet { get; set; }

    /// <summary>
    /// Postleitzahl (Lieferadresse)
    /// </summary>
    [StringLength(20)]
    [Display(Name = "Liefer-PLZ")]
    public string? ShippingZipCode { get; set; }

    /// <summary>
    /// Stadt (Lieferadresse)
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Lieferstadt")]
    public string? ShippingCity { get; set; }

    /// <summary>
    /// Land (Lieferadresse)
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Lieferland")]
    public string? ShippingCountry { get; set; }

    /// <summary>
    /// Umsatzsteuer-Identifikationsnummer
    /// </summary>
    [StringLength(30)]
    [Display(Name = "USt-ID")]
    public string? VatId { get; set; }

    /// <summary>
    /// Steuernummer
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Steuernummer")]
    public string? TaxNumber { get; set; }

    /// <summary>
    /// Zahlungsbedingungen in Tagen
    /// </summary>
    [Display(Name = "Zahlungsziel (Tage)")]
    [Range(0, 365, ErrorMessage = "Das Zahlungsziel muss zwischen 0 und 365 Tagen liegen")]
    public int PaymentTermDays { get; set; } = 14;

    /// <summary>
    /// Skonto-Prozentsatz
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Display(Name = "Skonto (%)")]
    [Range(0, 100, ErrorMessage = "Der Skonto muss zwischen 0 und 100 Prozent liegen")]
    public decimal? DiscountPercentage { get; set; }

    /// <summary>
    /// Skonto-Tage (innerhalb dieser Tage gilt der Skonto)
    /// </summary>
    [Display(Name = "Skonto-Tage")]
    [Range(0, 365, ErrorMessage = "Die Skonto-Tage müssen zwischen 0 und 365 liegen")]
    public int? DiscountDays { get; set; }

    /// <summary>
    /// Kreditlimit für den Kunden
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Kreditlimit")]
    [Range(0, double.MaxValue, ErrorMessage = "Das Kreditlimit muss größer oder gleich 0 sein")]
    public decimal? CreditLimit { get; set; }

    /// <summary>
    /// Status des Kunden
    /// </summary>
    [Required]
    [Display(Name = "Status")]
    public CustomerStatus Status { get; set; } = CustomerStatus.Active;

    /// <summary>
    /// Datum des letzten Kontakts
    /// </summary>
    [Display(Name = "Letzter Kontakt")]
    public DateTime? LastContactDate { get; set; }

    /// <summary>
    /// Interne Notizen zum Kunden
    /// </summary>
    [StringLength(1000)]
    [Display(Name = "Notizen")]
    public string? Notes { get; set; }

    // Computed Properties
    /// <summary>
    /// Vollständiger Name der Kontaktperson
    /// </summary>
    [NotMapped]
    [Display(Name = "Kontaktperson")]
    public string ContactFullName => $"{ContactFirstName} {ContactLastName}".Trim();

    /// <summary>
    /// Vollständige Rechnungsadresse
    /// </summary>
    [NotMapped]
    [Display(Name = "Rechnungsadresse")]
    public string BillingAddress => $"{BillingStreet}, {BillingZipCode} {BillingCity}, {BillingCountry}";

    /// <summary>
    /// Vollständige Lieferadresse (falls vorhanden)
    /// </summary>
    [NotMapped]
    [Display(Name = "Lieferadresse")]
    public string? ShippingAddress => 
        !string.IsNullOrEmpty(ShippingStreet) ? 
        $"{ShippingStreet}, {ShippingZipCode} {ShippingCity}, {ShippingCountry}" : 
        null;

    /// <summary>
    /// Verwendete Adresse für Lieferung (Lieferadresse falls vorhanden, sonst Rechnungsadresse)
    /// </summary>
    [NotMapped]
    public string EffectiveShippingAddress => ShippingAddress ?? BillingAddress;

    // Navigation Properties
    /// <summary>
    /// Rechnungen dieses Kunden
    /// </summary>
    public virtual ICollection<SMBErp.Domain.Sales.Invoice> Invoices { get; set; } = new List<SMBErp.Domain.Sales.Invoice>();

    // Business Logic Methods
    /// <summary>
    /// Berechnet das Fälligkeitsdatum für eine Rechnung basierend auf den Zahlungsbedingungen
    /// </summary>
    public DateTime CalculateDueDate(DateTime invoiceDate)
    {
        return invoiceDate.AddDays(PaymentTermDays);
    }

    /// <summary>
    /// Berechnet den Skonto-Betrag für einen Rechnungsbetrag
    /// </summary>
    public decimal CalculateDiscountAmount(decimal invoiceAmount)
    {
        if (!DiscountPercentage.HasValue)
            return 0;

        return invoiceAmount * (DiscountPercentage.Value / 100);
    }

    /// <summary>
    /// Prüft ob der Skonto noch gültig ist
    /// </summary>
    public bool IsDiscountValid(DateTime invoiceDate)
    {
        if (!DiscountDays.HasValue || !DiscountPercentage.HasValue)
            return false;

        return DateTime.UtcNow <= invoiceDate.AddDays(DiscountDays.Value);
    }
}
