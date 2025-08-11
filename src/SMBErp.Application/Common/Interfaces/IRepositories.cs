namespace SMBErp.Application.Common.Interfaces;

/// <summary>
/// Generisches Repository-Interface für gemeinsame CRUD-Operationen
/// </summary>
public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}

/// <summary>
/// Repository-Interfaces für die Application Layer
/// </summary>
public interface ICustomerRepository : IGenericRepository<SMBErp.Domain.Customers.Customer>
{
    Task<SMBErp.Domain.Customers.Customer?> GetLastCustomerAsync();
}

public interface IInvoiceRepository  
{
    Task<SMBErp.Domain.Sales.Invoice> AddAsync(SMBErp.Domain.Sales.Invoice invoice);
    Task<SMBErp.Domain.Sales.Invoice?> GetByIdAsync(int id);
    Task<List<SMBErp.Domain.Sales.Invoice>> GetByCustomerAsync(int customerId);
    Task<string> GetNextInvoiceNumberAsync();
}

public interface IItemRepository
{
    Task<SMBErp.Domain.Inventory.Item?> GetByIdAsync(int id);
    Task<List<SMBErp.Domain.Inventory.Item>> GetActiveItemsAsync();
}
