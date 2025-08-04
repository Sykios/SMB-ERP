using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMBErp.Models;

/// <summary>
/// Dienstleistung ohne physischen Lagerbestand
/// </summary>
public class Service : Item
{
    /// <summary>
    /// Geschätzte Dauer in Stunden
    /// </summary>
    [Column(TypeName = "decimal(8,2)")]
    [Display(Name = "Geschätzte Dauer (Stunden)")]
    [Range(0, double.MaxValue, ErrorMessage = "Die Dauer muss größer oder gleich 0 sein")]
    public decimal? EstimatedDurationHours { get; set; }

    /// <summary>
    /// Mindestdauer für die Abrechnung
    /// </summary>
    [Column(TypeName = "decimal(8,2)")]
    [Display(Name = "Mindestdauer (Stunden)")]
    [Range(0, double.MaxValue, ErrorMessage = "Die Mindestdauer muss größer oder gleich 0 sein")]
    public decimal? MinimumDurationHours { get; set; }

    /// <summary>
    /// Abrechnungsrhythmus (z.B. jede angefangene Stunde, 15-Minuten-Takt)
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Abrechnungsrhythmus")]
    public string? BillingRhythm { get; set; }

    /// <summary>
    /// Ist dies eine wiederkehrende Dienstleistung?
    /// </summary>
    [Display(Name = "Wiederkehrende Dienstleistung")]
    public bool IsRecurring { get; set; } = false;

    /// <summary>
    /// Intervall für wiederkehrende Dienstleistungen (z.B. "monatlich", "wöchentlich")
    /// </summary>
    [StringLength(50)]
    [Display(Name = "Wiederholungsintervall")]
    public string? RecurrenceInterval { get; set; }

    /// <summary>
    /// Erforderliche Qualifikationen
    /// </summary>
    [StringLength(500)]
    [Display(Name = "Erforderliche Qualifikationen")]
    public string? RequiredQualifications { get; set; }

    /// <summary>
    /// Arbeitsort (beim Kunden, im Büro, remote)
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Arbeitsort")]
    public string? WorkLocation { get; set; }

    /// <summary>
    /// Kann die Dienstleistung remote erbracht werden?
    /// </summary>
    [Display(Name = "Remote möglich")]
    public bool CanBeRemote { get; set; } = false;

    /// <summary>
    /// Materialkosten, die zusätzlich anfallen können
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Zusätzliche Materialkosten")]
    [Range(0, double.MaxValue, ErrorMessage = "Die Materialkosten müssen größer oder gleich 0 sein")]
    public decimal? AdditionalMaterialCosts { get; set; }

    /// <summary>
    /// Reisekosten pro Kilometer
    /// </summary>
    [Column(TypeName = "decimal(8,2)")]
    [Display(Name = "Reisekosten pro km")]
    [Range(0, double.MaxValue, ErrorMessage = "Die Reisekosten müssen größer oder gleich 0 sein")]
    public decimal? TravelCostPerKm { get; set; }

    /// <summary>
    /// Pauschale Anfahrtskosten
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Pauschale Anfahrtskosten")]
    [Range(0, double.MaxValue, ErrorMessage = "Die Anfahrtskosten müssen größer oder gleich 0 sein")]
    public decimal? FlatTravelCost { get; set; }
}
