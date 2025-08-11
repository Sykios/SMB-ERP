using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SMBErp.Domain.Common;
using SMBErp.Domain.Shared;

namespace SMBErp.Domain.Inventory;

/// <summary>
/// Basisklasse für Artikel (Produkte und Dienstleistungen)
/// </summary>
public abstract class Item : BaseEntity
{
    /// <summary>
    /// Artikelnummer (eindeutig)
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name = "Artikelnummer")]
    public required string ItemNumber { get; set; }

    /// <summary>
    /// Name des Artikels
    /// </summary>
    [Required]
    [StringLength(200)]
    [Display(Name = "Artikelname")]
    public required string Name { get; set; }

    /// <summary>
    /// Detaillierte Beschreibung
    /// </summary>
    [StringLength(1000)]
    [Display(Name = "Beschreibung")]
    public string? Description { get; set; }

    /// <summary>
    /// Verkaufspreis (netto)
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Verkaufspreis (netto)")]
    [Range(0, double.MaxValue, ErrorMessage = "Der Verkaufspreis muss größer oder gleich 0 sein")]
    public decimal SalePrice { get; set; }

    /// <summary>
    /// Einkaufspreis (netto) - optional
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Einkaufspreis (netto)")]
    [Range(0, double.MaxValue, ErrorMessage = "Der Einkaufspreis muss größer oder gleich 0 sein")]
    public decimal? PurchasePrice { get; set; }

    /// <summary>
    /// Mehrwertsteuersatz in Prozent
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(5,2)")]
    [Display(Name = "MwSt-Satz (%)")]
    [Range(0, 100, ErrorMessage = "Der MwSt-Satz muss zwischen 0 und 100 Prozent liegen")]
    public decimal VatRate { get; set; } = 20.0m; // Standard österreichischer MwSt-Satz

    /// <summary>
    /// Einheit des Artikels
    /// </summary>
    [Required]
    [Display(Name = "Einheit")]
    public Unit Unit { get; set; } = Unit.Piece;

    /// <summary>
    /// Kategorie oder Gruppe
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Kategorie")]
    public string? Category { get; set; }

    /// <summary>
    /// Ist der Artikel aktiv?
    /// </summary>
    [Display(Name = "Aktiv")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Notizen zum Artikel
    /// </summary>
    [StringLength(500)]
    [Display(Name = "Notizen")]
    public string? Notes { get; set; }

    /// <summary>
    /// Verkaufspreis inklusive Mehrwertsteuer
    /// </summary>
    [NotMapped]
    [Display(Name = "Verkaufspreis (brutto)")]
    public decimal SalePriceIncludingVat => SalePrice * (1 + VatRate / 100);

    /// <summary>
    /// Einkaufspreis inklusive Mehrwertsteuer
    /// </summary>
    [NotMapped]
    [Display(Name = "Einkaufspreis (brutto)")]
    public decimal? PurchasePriceIncludingVat => PurchasePrice.HasValue ? PurchasePrice * (1 + VatRate / 100) : null;

    /// <summary>
    /// Gewinnmarge in Prozent (falls Einkaufspreis vorhanden)
    /// </summary>
    [NotMapped]
    [Display(Name = "Gewinnmarge (%)")]
    public decimal? ProfitMarginPercentage => PurchasePrice.HasValue && PurchasePrice > 0 
        ? ((SalePrice - PurchasePrice.Value) / PurchasePrice.Value) * 100 
        : null;

    // Navigation Properties
    /// <summary>
    /// Rechnungspositionen, die diesen Artikel verwenden
    /// </summary>
    public virtual ICollection<SMBErp.Domain.Sales.InvoiceItem> InvoiceItems { get; set; } = new List<SMBErp.Domain.Sales.InvoiceItem>();

    // Business Logic Methods
    /// <summary>
    /// Aktiviert den Artikel
    /// </summary>
    public virtual void Activate()
    {
        IsActive = true;
        MarkAsUpdated();
    }

    /// <summary>
    /// Deaktiviert den Artikel
    /// </summary>
    public virtual void Deactivate()
    {
        IsActive = false;
        MarkAsUpdated();
    }

    /// <summary>
    /// Aktualisiert den Verkaufspreis
    /// </summary>
    /// <param name="newPrice">Neuer Verkaufspreis</param>
    public virtual void UpdateSalePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Der Verkaufspreis darf nicht negativ sein");
        
        SalePrice = newPrice;
        MarkAsUpdated();
    }
}
