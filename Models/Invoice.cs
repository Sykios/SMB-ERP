using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SMBErp.Models.Enums;

namespace SMBErp.Models;

/// <summary>
/// Rechnung
/// </summary>
public class Invoice
{
    /// <summary>
    /// Eindeutige ID der Rechnung
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Rechnungsnummer (eindeutig, automatisch generiert)
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name = "Rechnungsnummer")]
    public string InvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// Referenz auf den Kunden
    /// </summary>
    [Required]
    [Display(Name = "Kunde")]
    public int CustomerId { get; set; }

    /// <summary>
    /// Rechnungsdatum
    /// </summary>
    [Required]
    [Display(Name = "Rechnungsdatum")]
    [DataType(DataType.Date)]
    public DateTime InvoiceDate { get; set; } = DateTime.Today;

    /// <summary>
    /// Fälligkeitsdatum
    /// </summary>
    [Required]
    [Display(Name = "Fälligkeitsdatum")]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Leistungsdatum (kann abweichen vom Rechnungsdatum)
    /// </summary>
    [Display(Name = "Leistungsdatum")]
    [DataType(DataType.Date)]
    public DateTime? ServiceDate { get; set; }

    /// <summary>
    /// Status der Rechnung
    /// </summary>
    [Required]
    [Display(Name = "Status")]
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

    /// <summary>
    /// Nettobetrag (Summe aller Positionen ohne MwSt)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Nettobetrag")]
    public decimal NetAmount { get; set; }

    /// <summary>
    /// Mehrwertsteuerbetrag
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Mehrwertsteuer")]
    public decimal VatAmount { get; set; }

    /// <summary>
    /// Gesamtbetrag (Netto + MwSt)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Gesamtbetrag")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Bereits bezahlter Betrag
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Bezahlter Betrag")]
    public decimal PaidAmount { get; set; } = 0;

    /// <summary>
    /// Skonto-Prozentsatz für diese Rechnung
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Display(Name = "Skonto (%)")]
    [Range(0, 100, ErrorMessage = "Der Skonto muss zwischen 0 und 100 Prozent liegen")]
    public decimal? DiscountPercentage { get; set; }

    /// <summary>
    /// Skonto-Tage für diese Rechnung
    /// </summary>
    [Display(Name = "Skonto-Tage")]
    [Range(0, 365, ErrorMessage = "Die Skonto-Tage müssen zwischen 0 und 365 liegen")]
    public int? DiscountDays { get; set; }

    /// <summary>
    /// Zahlungsbedingungen in Tagen für diese Rechnung
    /// </summary>
    [Display(Name = "Zahlungsziel (Tage)")]
    [Range(0, 365, ErrorMessage = "Das Zahlungsziel muss zwischen 0 und 365 Tagen liegen")]
    public int PaymentTermDays { get; set; } = 14;

    /// <summary>
    /// Betreff/Überschrift der Rechnung
    /// </summary>
    [StringLength(200)]
    [Display(Name = "Betreff")]
    public string? Subject { get; set; }

    /// <summary>
    /// Einleitungstext der Rechnung
    /// </summary>
    [StringLength(1000)]
    [Display(Name = "Einleitungstext")]
    public string? IntroductionText { get; set; }

    /// <summary>
    /// Schlusstext der Rechnung
    /// </summary>
    [StringLength(1000)]
    [Display(Name = "Schlusstext")]
    public string? ConclusionText { get; set; }

    /// <summary>
    /// Interne Bemerkungen (nicht auf der Rechnung)
    /// </summary>
    [StringLength(1000)]
    [Display(Name = "Interne Bemerkungen")]
    public string? InternalNotes { get; set; }

    /// <summary>
    /// Referenz/Bestellnummer des Kunden
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Kundenreferenz")]
    public string? CustomerReference { get; set; }

    /// <summary>
    /// Projektnummer (falls zutreffend)
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Projektnummer")]
    public string? ProjectNumber { get; set; }

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
    /// Datum, an dem die Rechnung versendet wurde
    /// </summary>
    [Display(Name = "Versendet am")]
    public DateTime? SentDate { get; set; }

    /// <summary>
    /// Datum der vollständigen Bezahlung
    /// </summary>
    [Display(Name = "Bezahlt am")]
    public DateTime? PaidDate { get; set; }

    // Navigation Properties
    /// <summary>
    /// Kunde, an den die Rechnung gerichtet ist
    /// </summary>
    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; } = null!;

    /// <summary>
    /// Rechnungspositionen
    /// </summary>
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    // Calculated Properties
    /// <summary>
    /// Offener Betrag (noch zu bezahlen)
    /// </summary>
    [NotMapped]
    [Display(Name = "Offener Betrag")]
    public decimal OutstandingAmount => TotalAmount - PaidAmount;

    /// <summary>
    /// Ist die Rechnung überfällig?
    /// </summary>
    [NotMapped]
    [Display(Name = "Überfällig")]
    public bool IsOverdue => Status != InvoiceStatus.Paid && 
                            Status != InvoiceStatus.Cancelled && 
                            DueDate < DateTime.Today;

    /// <summary>
    /// Anzahl Tage überfällig
    /// </summary>
    [NotMapped]
    [Display(Name = "Tage überfällig")]
    public int DaysOverdue => IsOverdue ? (DateTime.Today - DueDate).Days : 0;

    /// <summary>
    /// Ist die Rechnung vollständig bezahlt?
    /// </summary>
    [NotMapped]
    [Display(Name = "Vollständig bezahlt")]
    public bool IsFullyPaid => Math.Abs(OutstandingAmount) < 0.01m; // Rundungstolerance

    /// <summary>
    /// Skonto-Betrag (falls anwendbar)
    /// </summary>
    [NotMapped]
    [Display(Name = "Skonto-Betrag")]
    public decimal? DiscountAmount => DiscountPercentage.HasValue ? 
        TotalAmount * (DiscountPercentage.Value / 100) : null;

    /// <summary>
    /// Skonto-Fälligkeitsdatum
    /// </summary>
    [NotMapped]
    [Display(Name = "Skonto bis")]
    public DateTime? DiscountDueDate => DiscountDays.HasValue ? 
        InvoiceDate.AddDays(DiscountDays.Value) : null;

    /// <summary>
    /// Ist Skonto noch gültig?
    /// </summary>
    [NotMapped]
    [Display(Name = "Skonto gültig")]
    public bool IsDiscountValid => DiscountDueDate.HasValue && 
                                  DateTime.Today <= DiscountDueDate.Value;
}
