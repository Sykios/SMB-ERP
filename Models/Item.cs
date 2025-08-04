using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SMBErp.Models.Enums;

namespace SMBErp.Models;

/// <summary>
/// Basisklasse für Artikel (Produkte und Dienstleistungen)
/// </summary>
public abstract class Item
{
    /// <summary>
    /// Eindeutige ID des Artikels
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Artikelnummer (eindeutig)
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name = "Artikelnummer")]
    public string ItemNumber { get; set; } = string.Empty;

    /// <summary>
    /// Name des Artikels
    /// </summary>
    [Required]
    [StringLength(200)]
    [Display(Name = "Artikelname")]
    public string Name { get; set; } = string.Empty;

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
    public decimal VatRate { get; set; } = 19.0m;

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
    /// Notizen zum Artikel
    /// </summary>
    [StringLength(500)]
    [Display(Name = "Notizen")]
    public string? Notes { get; set; }

    // Navigation Properties
    /// <summary>
    /// Rechnungspositionen, die diesen Artikel verwenden
    /// </summary>
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

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
}
