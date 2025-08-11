using Microsoft.EntityFrameworkCore;
using SMBErp.Application.Common.Interfaces;
using SMBErp.Domain.Inventory;

namespace SMBErp.Infrastructure.Data.Repositories;

/// <summary>
/// Repository für Produkt/Dienstleistungsverwaltung
/// </summary>
public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    public ItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Alle aktiven Artikel abrufen
    /// </summary>
    public async Task<List<Item>> GetActiveItemsAsync()
    {
        return await _dbSet
            .Where(i => !i.IsDeleted && i.IsActive)
            .OrderBy(i => i.ItemNumber)
            .ToListAsync();
    }
}
