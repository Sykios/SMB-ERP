using System.ComponentModel.DataAnnotations;
using SMBErp.Models.Enums;

namespace SMBErp.Models;

/// <summary>
/// E-Mail-Vorlage für verschiedene Kommunikationszwecke
/// </summary>
public class EmailTemplate
{
    /// <summary>
    /// Eindeutige ID der E-Mail-Vorlage
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Name der Vorlage (für interne Verwaltung)
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "Vorlagenname")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Typ der E-Mail-Vorlage
    /// </summary>
    [Required]
    [Display(Name = "Typ")]
    public EmailTemplateType Type { get; set; }

    /// <summary>
    /// Betreffzeile der E-Mail
    /// </summary>
    [Required]
    [StringLength(200)]
    [Display(Name = "Betreff")]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Inhalt der E-Mail (HTML oder Text)
    /// </summary>
    [Required]
    [Display(Name = "E-Mail-Inhalt")]
    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// Ist der Inhalt HTML-formatiert?
    /// </summary>
    [Display(Name = "HTML-Format")]
    public bool IsHtml { get; set; } = true;

    /// <summary>
    /// Ist dies die Standard-Vorlage für diesen Typ?
    /// </summary>
    [Display(Name = "Standard-Vorlage")]
    public bool IsDefault { get; set; } = false;

    /// <summary>
    /// Ist die Vorlage aktiv?
    /// </summary>
    [Display(Name = "Aktiv")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Sprache der Vorlage
    /// </summary>
    [StringLength(10)]
    [Display(Name = "Sprache")]
    public string Language { get; set; } = "de-DE";

    /// <summary>
    /// Beschreibung der Vorlage
    /// </summary>
    [StringLength(500)]
    [Display(Name = "Beschreibung")]
    public string? Description { get; set; }

    /// <summary>
    /// Verfügbare Platzhalter (Hilfsinformation)
    /// </summary>
    [StringLength(1000)]
    [Display(Name = "Verfügbare Platzhalter")]
    public string? AvailablePlaceholders { get; set; }

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

    /// <summary>
    /// Von wem wurde die Vorlage erstellt (Benutzer-ID)
    /// </summary>
    [StringLength(450)]
    [Display(Name = "Erstellt von")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Von wem wurde die Vorlage zuletzt geändert (Benutzer-ID)
    /// </summary>
    [StringLength(450)]
    [Display(Name = "Geändert von")]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Automatisch anhängen von Rechnungs-PDF (nur bei Invoice-Typ)
    /// </summary>
    [Display(Name = "PDF automatisch anhängen")]
    public bool AttachPdfAutomatically { get; set; } = true;

    /// <summary>
    /// BCC-E-Mail-Adresse (Kopie an interne Adresse)
    /// </summary>
    [StringLength(200)]
    [EmailAddress]
    [Display(Name = "BCC-Adresse")]
    public string? BccEmail { get; set; }

    /// <summary>
    /// Antwort-E-Mail-Adresse (falls abweichend)
    /// </summary>
    [StringLength(200)]
    [EmailAddress]
    [Display(Name = "Antwort-Adresse")]
    public string? ReplyToEmail { get; set; }

    /// <summary>
    /// Priorität der E-Mail (Normal, Hoch, Niedrig)
    /// </summary>
    [StringLength(20)]
    [Display(Name = "Priorität")]
    public string Priority { get; set; } = "Normal";

    /// <summary>
    /// Typ-spezifische Anzeigenamen für UI
    /// </summary>
    public static Dictionary<EmailTemplateType, string> TypeDisplayNames => new()
    {
        { EmailTemplateType.Invoice, "Rechnungsversand" },
        { EmailTemplateType.PaymentReminder, "Zahlungserinnerung" },
        { EmailTemplateType.Dunning, "Mahnung" },
        { EmailTemplateType.Quote, "Angebot" },
        { EmailTemplateType.General, "Allgemeine Korrespondenz" }
    };

    /// <summary>
    /// Standard-Platzhalter für verschiedene Template-Typen
    /// </summary>
    public static Dictionary<EmailTemplateType, string> DefaultPlaceholders => new()
    {
        { EmailTemplateType.Invoice, "{{CustomerName}}, {{InvoiceNumber}}, {{InvoiceDate}}, {{DueDate}}, {{TotalAmount}}, {{CompanyName}}" },
        { EmailTemplateType.PaymentReminder, "{{CustomerName}}, {{InvoiceNumber}}, {{DueDate}}, {{DaysOverdue}}, {{OutstandingAmount}}, {{CompanyName}}" },
        { EmailTemplateType.Dunning, "{{CustomerName}}, {{InvoiceNumber}}, {{DueDate}}, {{DaysOverdue}}, {{OutstandingAmount}}, {{CompanyName}}" },
        { EmailTemplateType.Quote, "{{CustomerName}}, {{QuoteNumber}}, {{QuoteDate}}, {{ValidUntil}}, {{TotalAmount}}, {{CompanyName}}" },
        { EmailTemplateType.General, "{{CustomerName}}, {{CompanyName}}, {{Date}}" }
    };
}
