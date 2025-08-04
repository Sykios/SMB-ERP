using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMBErp.Models;

/// <summary>
/// Physisches Produkt mit Lagerbestand
/// </summary>
public class Product : Item
{
    /// <summary>
    /// Barcode oder EAN-Code
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Barcode/EAN")]
    public string? Barcode { get; set; }

    /// <summary>
    /// Aktueller Lagerbestand
    /// </summary>
    [Column(TypeName = "decimal(18,3)")]
    [Display(Name = "Lagerbestand")]
    [Range(0, double.MaxValue, ErrorMessage = "Der Lagerbestand muss größer oder gleich 0 sein")]
    public decimal StockQuantity { get; set; } = 0;

    /// <summary>
    /// Mindestbestand - Warnung bei Unterschreitung
    /// </summary>
    [Column(TypeName = "decimal(18,3)")]
    [Display(Name = "Mindestbestand")]
    [Range(0, double.MaxValue, ErrorMessage = "Der Mindestbestand muss größer oder gleich 0 sein")]
    public decimal MinimumStock { get; set; } = 0;

    /// <summary>
    /// Maximaler Lagerbestand
    /// </summary>
    [Column(TypeName = "decimal(18,3)")]
    [Display(Name = "Maximaler Lagerbestand")]
    [Range(0, double.MaxValue, ErrorMessage = "Der maximale Lagerbestand muss größer oder gleich 0 sein")]
    public decimal? MaximumStock { get; set; }

    /// <summary>
    /// Lagerort oder -platz
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Lagerort")]
    public string? StorageLocation { get; set; }

    /// <summary>
    /// Gewicht des Produkts in Kilogramm
    /// </summary>
    [Column(TypeName = "decimal(18,3)")]
    [Display(Name = "Gewicht (kg)")]
    [Range(0, double.MaxValue, ErrorMessage = "Das Gewicht muss größer oder gleich 0 sein")]
    public decimal? Weight { get; set; }

    /// <summary>
    /// Abmessungen (Länge x Breite x Höhe) in cm
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Abmessungen (L×B×H cm)")]
    public string? Dimensions { get; set; }

    /// <summary>
    /// Hersteller des Produkts
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Hersteller")]
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Herstellernummer oder -artikelnummer
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Herstellernummer")]
    public string? ManufacturerPartNumber { get; set; }

    /// <summary>
    /// Lieferant-ID (Verweis auf zukünftige Supplier-Tabelle)
    /// </summary>
    [Display(Name = "Lieferant-ID")]
    public int? SupplierId { get; set; }

    /// <summary>
    /// Warnung bei niedrigem Lagerbestand?
    /// </summary>
    [NotMapped]
    [Display(Name = "Niedriger Lagerbestand")]
    public bool IsLowStock => StockQuantity <= MinimumStock;

    /// <summary>
    /// Verfügbare Menge (für Verkauf)
    /// </summary>
    [NotMapped]
    [Display(Name = "Verfügbare Menge")]
    public decimal AvailableQuantity => Math.Max(0, StockQuantity);
}
