using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMBErp.Domain.Inventory;

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
    /// Abmessungen (L×B×H in cm)
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
    /// Herstellerartikelnummer
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Herstellerartikelnummer")]
    public string? ManufacturerItemNumber { get; set; }

    /// <summary>
    /// Herstellerteilenummer (für EF Configuration Kompatibilität)
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Herstellerteilenummer")]
    public string? ManufacturerPartNumber { get; set; }

    /// <summary>
    /// Lieferanten-ID (Referenz auf Lieferant)
    /// </summary>
    [Display(Name = "Lieferant")]
    public int? SupplierId { get; set; }

    /// <summary>
    /// Lieferantenartikelnummer
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Lieferantenartikelnummer")]
    public string? SupplierItemNumber { get; set; }

    /// <summary>
    /// Ist das Produkt unter dem Mindestbestand?
    /// </summary>
    [NotMapped]
    [Display(Name = "Unter Mindestbestand")]
    public bool IsBelowMinimumStock => StockQuantity < MinimumStock;

    /// <summary>
    /// Ist das Produkt über dem maximalen Lagerbestand?
    /// </summary>
    [NotMapped]
    [Display(Name = "Über Maximallagerbestand")]
    public bool IsAboveMaximumStock => MaximumStock.HasValue && StockQuantity > MaximumStock.Value;

    /// <summary>
    /// Lagerbestandsstatus
    /// </summary>
    [NotMapped]
    [Display(Name = "Lagerbestandsstatus")]
    public string StockStatus
    {
        get
        {
            if (StockQuantity <= 0) return "Nicht verfügbar";
            if (IsBelowMinimumStock) return "Niedrig";
            if (IsAboveMaximumStock) return "Überhöht";
            return "Normal";
        }
    }

    // Business Logic Methods
    /// <summary>
    /// Erhöht den Lagerbestand (z.B. bei Wareneingang)
    /// </summary>
    /// <param name="quantity">Zu erhöhende Menge</param>
    /// <param name="reason">Grund für die Erhöhung</param>
    public void IncreaseStock(decimal quantity, string? reason = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Die Menge muss größer als 0 sein");

        StockQuantity += quantity;
        
        if (!string.IsNullOrWhiteSpace(reason))
        {
            Notes = string.IsNullOrWhiteSpace(Notes) 
                ? $"Lagerbestand erhöht um {quantity}: {reason}" 
                : $"{Notes}\nLagerbestand erhöht um {quantity}: {reason}";
        }
        
        MarkAsUpdated();
    }

    /// <summary>
    /// Reduziert den Lagerbestand (z.B. bei Verkauf)
    /// </summary>
    /// <param name="quantity">Zu reduzierende Menge</param>
    /// <param name="reason">Grund für die Reduzierung</param>
    public void DecreaseStock(decimal quantity, string? reason = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Die Menge muss größer als 0 sein");

        if (quantity > StockQuantity)
            throw new InvalidOperationException("Nicht genügend Lagerbestand vorhanden");

        StockQuantity -= quantity;
        
        if (!string.IsNullOrWhiteSpace(reason))
        {
            Notes = string.IsNullOrWhiteSpace(Notes) 
                ? $"Lagerbestand reduziert um {quantity}: {reason}" 
                : $"{Notes}\nLagerbestand reduziert um {quantity}: {reason}";
        }
        
        MarkAsUpdated();
    }

    /// <summary>
    /// Setzt den Lagerbestand auf einen bestimmten Wert (z.B. bei Inventur)
    /// </summary>
    /// <param name="newQuantity">Neuer Lagerbestand</param>
    /// <param name="reason">Grund für die Korrektur</param>
    public void SetStock(decimal newQuantity, string? reason = null)
    {
        if (newQuantity < 0)
            throw new ArgumentException("Der Lagerbestand darf nicht negativ sein");

        var oldQuantity = StockQuantity;
        StockQuantity = newQuantity;
        
        if (!string.IsNullOrWhiteSpace(reason))
        {
            Notes = string.IsNullOrWhiteSpace(Notes) 
                ? $"Lagerbestand korrigiert von {oldQuantity} auf {newQuantity}: {reason}" 
                : $"{Notes}\nLagerbestand korrigiert von {oldQuantity} auf {newQuantity}: {reason}";
        }
        
        MarkAsUpdated();
    }

    /// <summary>
    /// Prüft ob ausreichend Lagerbestand für eine bestimmte Menge vorhanden ist
    /// </summary>
    /// <param name="requiredQuantity">Benötigte Menge</param>
    /// <returns>True wenn ausreichend vorhanden, sonst false</returns>
    public bool HasSufficientStock(decimal requiredQuantity)
    {
        return StockQuantity >= requiredQuantity;
    }
}
