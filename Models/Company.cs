using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMBErp.Models;

/// <summary>
/// Erweiterte Firmeneinstellungen (zusätzlich zu den Konfigurationseinstellungen)
/// </summary>
public class Company
{
    /// <summary>
    /// Eindeutige ID der Firma (normalerweise nur eine)
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Vollständiger Firmenname
    /// </summary>
    [Required]
    [StringLength(200)]
    [Display(Name = "Firmenname")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Rechtsform (GmbH, AG, e.K., etc.)
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Rechtsform")]
    public string? LegalForm { get; set; }

    /// <summary>
    /// Geschäftsführer/Inhaber
    /// </summary>
    [StringLength(200)]
    [Display(Name = "Geschäftsführer")]
    public string? ManagingDirector { get; set; }

    /// <summary>
    /// Straße und Hausnummer
    /// </summary>
    [Required]
    [StringLength(200)]
    [Display(Name = "Straße")]
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Postleitzahl
    /// </summary>
    [Required]
    [StringLength(20)]
    [Display(Name = "PLZ")]
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Stadt
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "Stadt")]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Land
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "Land")]
    public string Country { get; set; } = "Österreich";

    /// <summary>
    /// Haupttelefonnummer
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Telefon")]
    public string? Phone { get; set; }

    /// <summary>
    /// Faxnummer
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Fax")]
    public string? Fax { get; set; }

    /// <summary>
    /// Haupt-Mail-Adresse
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(200)]
    [Display(Name = "E-Mail")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Website/Homepage
    /// </summary>
    [Url]
    [StringLength(200)]
    [Display(Name = "Website")]
    public string? Website { get; set; }

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
    /// Handelsregistereintrag
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Handelsregister")]
    public string? CommercialRegister { get; set; }

    /// <summary>
    /// Amtsgericht (für Handelsregister)
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Amtsgericht")]
    public string? Court { get; set; }

    // Bankverbindung
    /// <summary>
    /// Name der Bank
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Bankname")]
    public string? BankName { get; set; }

    /// <summary>
    /// IBAN
    /// </summary>
    [StringLength(34)]
    [Display(Name = "IBAN")]
    public string? IBAN { get; set; }

    /// <summary>
    /// BIC/SWIFT-Code
    /// </summary>
    [StringLength(11)]
    [Display(Name = "BIC")]
    public string? BIC { get; set; }

    /// <summary>
    /// Kontoinhaber (falls abweichend)
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Kontoinhaber")]
    public string? AccountHolder { get; set; }

    // Weitere Bankverbindungen (optional)
    /// <summary>
    /// Zweite Bankverbindung - Bankname
    /// </summary>
    [StringLength(100)]
    [Display(Name = "2. Bankname")]
    public string? BankName2 { get; set; }

    /// <summary>
    /// Zweite Bankverbindung - IBAN
    /// </summary>
    [StringLength(34)]
    [Display(Name = "2. IBAN")]
    public string? IBAN2 { get; set; }

    /// <summary>
    /// Zweite Bankverbindung - BIC
    /// </summary>
    [StringLength(11)]
    [Display(Name = "2. BIC")]
    public string? BIC2 { get; set; }

    // Firmenlogo und Design
    /// <summary>
    /// Pfad zum Firmenlogo
    /// </summary>
    [StringLength(500)]
    [Display(Name = "Logo-Pfad")]
    public string? LogoPath { get; set; }

    /// <summary>
    /// Firmenfarbe (Hex-Code)
    /// </summary>
    [StringLength(7)]
    [Display(Name = "Firmenfarbe")]
    public string? CompanyColor { get; set; }

    /// <summary>
    /// Sekundärfarbe (Hex-Code)
    /// </summary>
    [StringLength(7)]
    [Display(Name = "Sekundärfarbe")]
    public string? SecondaryColor { get; set; }

    // Standard-Einstellungen
    /// <summary>
    /// Standard-Zahlungsbedingungen in Tagen
    /// </summary>
    [Display(Name = "Standard-Zahlungsziel (Tage)")]
    [Range(0, 365, ErrorMessage = "Das Zahlungsziel muss zwischen 0 und 365 Tagen liegen")]
    public int DefaultPaymentTermDays { get; set; } = 14;

    /// <summary>
    /// Standard-MwSt-Satz
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Display(Name = "Standard-MwSt-Satz (%)")]
    [Range(0, 100, ErrorMessage = "Der MwSt-Satz muss zwischen 0 und 100 Prozent liegen")]
    public decimal DefaultVatRate { get; set; } = 19.0m;

    /// <summary>
    /// Währung
    /// </summary>
    [Required]
    [StringLength(3)]
    [Display(Name = "Währung")]
    public string Currency { get; set; } = "EUR";

    /// <summary>
    /// Zusätzliche Informationen für Rechnungen
    /// </summary>
    [StringLength(1000)]
    [Display(Name = "Zusatzinformationen")]
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Kleinunternehmer nach §19 UStG?
    /// </summary>
    [Display(Name = "Kleinunternehmer")]
    public bool IsSmallBusiness { get; set; } = false;

    /// <summary>
    /// Kleinunternehmer-Text für Rechnungen
    /// </summary>
    [StringLength(500)]
    [Display(Name = "Kleinunternehmer-Text")]
    public string? SmallBusinessText { get; set; }

    /// <summary>
    /// Erstellungsdatum
    /// </summary>
    [Display(Name = "Erstellt am")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Datum der letzten Änderung
    /// </summary>
    [Display(Name = "Zuletzt geändert")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Computed Properties
    /// <summary>
    /// Vollständige Adresse
    /// </summary>
    [NotMapped]
    [Display(Name = "Vollständige Adresse")]
    public string FullAddress => $"{Street}, {ZipCode} {City}, {Country}";

    /// <summary>
    /// Vollständiger Firmenname mit Rechtsform
    /// </summary>
    [NotMapped]
    [Display(Name = "Vollständiger Name")]
    public string FullName => !string.IsNullOrEmpty(LegalForm) ? $"{Name} {LegalForm}" : Name;
}
