using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SMBErp.Models.Enums;

namespace SMBErp.Models;

/// <summary>
/// Rechnungsposition
/// </summary>
public class InvoiceItem
{
    /// <summary>
    /// Eindeutige ID der Rechnungsposition
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Referenz auf die Rechnung
    /// </summary>
    [Required]
    [Display(Name = "Rechnung")]
    public int InvoiceId { get; set; }

    /// <summary>
    /// Positionsnummer (Reihenfolge in der Rechnung)
    /// </summary>
    [Required]
    [Display(Name = "Position")]
    [Range(1, int.MaxValue, ErrorMessage = "Die Position muss größer als 0 sein")]
    public int Position { get; set; }

    /// <summary>
    /// Referenz auf den Artikel (optional - kann auch freie Position sein)
    /// </summary>
    [Display(Name = "Artikel")]
    public int? ItemId { get; set; }

    /// <summary>
    /// Artikelnummer (wird automatisch aus Item übernommen oder manuell eingegeben)
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Artikelnummer")]
    public string? ItemNumber { get; set; }

    /// <summary>
    /// Beschreibung der Position
    /// </summary>
    [Required]
    [StringLength(500)]
    [Display(Name = "Beschreibung")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Menge
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,3)")]
    [Display(Name = "Menge")]
    [Range(0.001, double.MaxValue, ErrorMessage = "Die Menge muss größer als 0 sein")]
    public decimal Quantity { get; set; } = 1;

    /// <summary>
    /// Einheit
    /// </summary>
    [Required]
    [Display(Name = "Einheit")]
    public Unit Unit { get; set; } = Unit.Piece;

    /// <summary>
    /// Einzelpreis (netto)
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Einzelpreis (netto)")]
    [Range(0, double.MaxValue, ErrorMessage = "Der Einzelpreis muss größer oder gleich 0 sein")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Rabatt in Prozent
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Display(Name = "Rabatt (%)")]
    [Range(0, 100, ErrorMessage = "Der Rabatt muss zwischen 0 und 100 Prozent liegen")]
    public decimal DiscountPercentage { get; set; } = 0;

    /// <summary>
    /// Mehrwertsteuersatz in Prozent
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(5,2)")]
    [Display(Name = "MwSt-Satz (%)")]
    [Range(0, 100, ErrorMessage = "Der MwSt-Satz muss zwischen 0 und 100 Prozent liegen")]
    public decimal VatRate { get; set; } = 19.0m;

    /// <summary>
    /// Zusätzliche Informationen zur Position
    /// </summary>
    [StringLength(200)]
    [Display(Name = "Zusatzinformationen")]
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Datum der Erstellung
    /// </summary>
    [Display(Name = "Erstellt am")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    /// <summary>
    /// Rechnung, zu der diese Position gehört
    /// </summary>
    [ForeignKey("InvoiceId")]
    public virtual Invoice Invoice { get; set; } = null!;

    /// <summary>
    /// Artikel, auf den sich diese Position bezieht (falls vorhanden)
    /// </summary>
    [ForeignKey("ItemId")]
    public virtual Item? Item { get; set; }

    // Calculated Properties
    /// <summary>
    /// Nettopreis (Einzelpreis * Menge)
    /// </summary>
    [NotMapped]
    [Display(Name = "Nettopreis")]
    public decimal NetPrice => UnitPrice * Quantity;

    /// <summary>
    /// Rabattbetrag
    /// </summary>
    [NotMapped]
    [Display(Name = "Rabattbetrag")]
    public decimal DiscountAmount => NetPrice * (DiscountPercentage / 100);

    /// <summary>
    /// Nettopreis nach Rabatt
    /// </summary>
    [NotMapped]
    [Display(Name = "Netto nach Rabatt")]
    public decimal NetPriceAfterDiscount => NetPrice - DiscountAmount;

    /// <summary>
    /// Mehrwertsteuerbetrag
    /// </summary>
    [NotMapped]
    [Display(Name = "MwSt-Betrag")]
    public decimal VatAmount => NetPriceAfterDiscount * (VatRate / 100);

    /// <summary>
    /// Gesamtpreis (Netto nach Rabatt + MwSt)
    /// </summary>
    [NotMapped]
    [Display(Name = "Gesamtpreis")]
    public decimal TotalPrice => NetPriceAfterDiscount + VatAmount;

    /// <summary>
    /// Einzelpreis inklusive MwSt (ohne Rabatt)
    /// </summary>
    [NotMapped]
    [Display(Name = "Einzelpreis (brutto)")]
    public decimal UnitPriceIncludingVat => UnitPrice * (1 + VatRate / 100);

    /// <summary>
    /// Einheiten-String für Anzeige
    /// </summary>
    [NotMapped]
    [Display(Name = "Einheit (Text)")]
    public string UnitDisplayText => Unit switch
    {
        Unit.Piece => "Stk",
        Unit.Kilogram => "kg",
        Unit.Meter => "m",
        Unit.SquareMeter => "m²",
        Unit.CubicMeter => "m³",
        Unit.Liter => "l",
        Unit.Hour => "h",
        Unit.Day => "Tage",
        Unit.Flat => "pauschal",
        _ => Unit.ToString()
    };
}
