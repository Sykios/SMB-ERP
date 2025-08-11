using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SMBErp.Domain.Common;
using SMBErp.Domain.Shared;

namespace SMBErp.Domain.Sales;

/// <summary>
/// Rechnungsposition
/// </summary>
public class InvoiceItem : BaseEntity
{
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
    public required string Description { get; set; }

    /// <summary>
    /// Menge
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,4)")]
    [Display(Name = "Menge")]
    [Range(0.0001, double.MaxValue, ErrorMessage = "Die Menge muss größer als 0 sein")]
    public decimal Quantity { get; set; }

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
    [Display(Name = "Einzelpreis")]
    [Range(0, double.MaxValue, ErrorMessage = "Der Einzelpreis muss größer oder gleich 0 sein")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Rabatt-Prozentsatz auf diese Position
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Display(Name = "Rabatt (%)")]
    [Range(0, 100, ErrorMessage = "Der Rabatt muss zwischen 0 und 100 Prozent liegen")]
    public decimal? DiscountPercentage { get; set; }

    /// <summary>
    /// Mehrwertsteuersatz in Prozent
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(5,2)")]
    [Display(Name = "MwSt-Satz (%)")]
    [Range(0, 100, ErrorMessage = "Der MwSt-Satz muss zwischen 0 und 100 Prozent liegen")]
    public decimal VatRate { get; set; } = 20; // Standard österreichischer MwSt-Satz

    /// <summary>
    /// Sortierreihenfolge (für die Anzeige)
    /// </summary>
    [Display(Name = "Sortierung")]
    public int SortOrder { get; set; }

    // Navigation Properties
    /// <summary>
    /// Rechnung zu der diese Position gehört
    /// </summary>
    [ForeignKey("InvoiceId")]
    public virtual Invoice Invoice { get; set; } = null!;

    /// <summary>
    /// Artikel zu dem diese Position gehört (optional)
    /// </summary>
    [ForeignKey("ItemId")]
    public virtual SMBErp.Domain.Inventory.Item? Item { get; set; }

    // Calculated Properties
    /// <summary>
    /// Nettobetrag vor Rabatt (Menge × Einzelpreis)
    /// </summary>
    [NotMapped]
    [Display(Name = "Nettobetrag (brutto)")]
    public decimal GrossNetAmount => Quantity * UnitPrice;

    /// <summary>
    /// Rabattbetrag
    /// </summary>
    [NotMapped]
    [Display(Name = "Rabattbetrag")]
    public decimal DiscountAmount => DiscountPercentage.HasValue ? 
        GrossNetAmount * (DiscountPercentage.Value / 100) : 0;

    /// <summary>
    /// Nettobetrag nach Rabatt
    /// </summary>
    [NotMapped]
    [Display(Name = "Nettobetrag")]
    public decimal TotalNetAmount => GrossNetAmount - DiscountAmount;

    /// <summary>
    /// Mehrwertsteuerbetrag
    /// </summary>
    [NotMapped]
    [Display(Name = "MwSt-Betrag")]
    public decimal VatAmount => TotalNetAmount * (VatRate / 100);

    /// <summary>
    /// Gesamtbetrag (Netto + MwSt)
    /// </summary>
    [NotMapped]
    [Display(Name = "Gesamtbetrag")]
    public decimal TotalGrossAmount => TotalNetAmount + VatAmount;

    /// <summary>
    /// Einheit als Text
    /// </summary>
    [NotMapped]
    [Display(Name = "Einheit")]
    public string UnitText => Unit switch
    {
        Unit.Piece => "Stk",
        Unit.Kilogram => "kg",
        Unit.Meter => "m",
        Unit.SquareMeter => "m²",
        Unit.CubicMeter => "m³",
        Unit.Liter => "l",
        Unit.Hour => "Std",
        Unit.Day => "Tag",
        Unit.Flat => "pauschal",
        _ => Unit.ToString()
    };

    // Business Logic Methods
    /// <summary>
    /// Berechnet alle Beträge neu basierend auf Menge, Preis und Rabatten
    /// </summary>
    public void RecalculateAmounts()
    {
        // Calculated properties handle the calculation automatically
        // This method exists for explicit recalculation if needed
        MarkAsUpdated();
    }

    /// <summary>
    /// Wendet einen Rabatt auf diese Position an
    /// </summary>
    /// <param name="discountPercentage">Rabatt in Prozent</param>
    public void ApplyDiscount(decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException("Rabatt muss zwischen 0 und 100 Prozent liegen");
        
        DiscountPercentage = discountPercentage;
        MarkAsUpdated();
    }

    /// <summary>
    /// Entfernt den Rabatt von dieser Position
    /// </summary>
    public void RemoveDiscount()
    {
        DiscountPercentage = null;
        MarkAsUpdated();
    }
}
