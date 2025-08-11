using Microsoft.EntityFrameworkCore;
using SMBErp.Application.Common.Interfaces;
using SMBErp.Domain.Customers;

namespace SMBErp.Infrastructure.Data.Repositories;

/// <summary>
/// Repository für Kundenverwaltung
/// </summary>
public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Letzten angelegten Kunden abrufen (für Kundennummern-Generierung)
    /// </summary>
    public async Task<Customer?> GetLastCustomerAsync()
    {
        return await _dbSet
            .Where(c => !c.IsDeleted)
            .OrderByDescending(c => c.Id)
            .FirstOrDefaultAsync();
    }
}
