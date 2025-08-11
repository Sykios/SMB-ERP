using Microsoft.EntityFrameworkCore;
using SMBErp.Application.Common.Interfaces;
using SMBErp.Domain.Common;

namespace SMBErp.Infrastructure.Data.Repositories;

/// <summary>
/// Generisches Repository für gemeinsame CRUD-Operationen
/// </summary>
/// <typeparam name="T">Entity-Typ</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// Entität anhand der ID abrufen
    /// </summary>
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    }

    /// <summary>
    /// Alle aktiven Entitäten abrufen
    /// </summary>
    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
    }

    /// <summary>
    /// Neue Entität hinzufügen
    /// </summary>
    public virtual async Task<T> AddAsync(T entity)
    {
        // BaseEntity Eigenschaften setzen
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsDeleted = false;

        var result = await _dbSet.AddAsync(entity);
        return result.Entity;
    }

    /// <summary>
    /// Entität aktualisieren
    /// </summary>
    public virtual Task UpdateAsync(T entity)
    {
        // BaseEntity Eigenschaften aktualisieren
        entity.UpdatedAt = DateTime.UtcNow;
        
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Entität löschen (Soft Delete)
    /// </summary>
    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            
            await UpdateAsync(entity);
        }
    }

    /// <summary>
    /// Änderungen speichern
    /// </summary>
    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
