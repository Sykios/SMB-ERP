using System.ComponentModel.DataAnnotations;

namespace SMBErp.Domain.Common;

/// <summary>
/// Basis-Entity-Klasse mit gemeinsamen Eigenschaften für alle Entitäten
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Primärschlüssel
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Erstellungsdatum (UTC)
    /// </summary>
    [Display(Name = "Erstellt am")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Letztes Änderungsdatum (UTC)
    /// </summary>
    [Display(Name = "Geändert am")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Benutzer der die Entität erstellt hat
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Erstellt von")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Benutzer der die Entität zuletzt geändert hat
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Geändert von")]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Soft-Delete Flag
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Löschdatum (UTC)
    /// </summary>
    [Display(Name = "Gelöscht am")]
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Benutzer der die Entität gelöscht hat
    /// </summary>
    [StringLength(100)]
    [Display(Name = "Gelöscht von")]
    public string? DeletedBy { get; set; }

    /// <summary>
    /// Markiert die Entität als gelöscht (Soft Delete)
    /// </summary>
    /// <param name="deletedBy">Benutzer der die Löschung durchführt</param>
    public virtual void MarkAsDeleted(string? deletedBy = null)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = deletedBy;
    }

    /// <summary>
    /// Stellt eine gelöschte Entität wieder her
    /// </summary>
    public virtual void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
        DeletedBy = null;
    }

    /// <summary>
    /// Aktualisiert die Änderungs-Metadaten
    /// </summary>
    /// <param name="updatedBy">Benutzer der die Änderung durchführt</param>
    public virtual void MarkAsUpdated(string? updatedBy = null)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}
