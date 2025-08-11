using System.ComponentModel.DataAnnotations;

namespace SMBErp.Domain.Common;

/// <summary>
/// Basis-Entität für alle Domain-Objekte mit Audit-Feldern
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Eindeutige ID der Entität
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Erstellungszeitpunkt (UTC)
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Zeitpunkt der letzten Änderung (UTC)
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Soft Delete - Zeitpunkt der Löschung (falls gelöscht)
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Ist die Entität gelöscht?
    /// </summary>
    public bool IsDeleted => DeletedAt.HasValue;

    /// <summary>
    /// Markiert die Entität als gelöscht (Soft Delete)
    /// </summary>
    public virtual void Delete()
    {
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Stellt eine gelöschte Entität wieder her
    /// </summary>
    public virtual void Restore()
    {
        DeletedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Markiert die Entität als geändert
    /// </summary>
    public virtual void MarkAsModified()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
