namespace SMBErp.Application.Common.Interfaces;

/// <summary>
/// Repository-Interfaces für die Application Layer
/// </summary>
public interface ICustomerRepository
{
    Task<SMBErp.Domain.Customers.Customer> AddAsync(SMBErp.Domain.Customers.Customer customer);
    Task<SMBErp.Domain.Customers.Customer?> GetByIdAsync(int id);
    Task<SMBErp.Domain.Customers.Customer?> GetLastCustomerAsync();
    Task<List<SMBErp.Domain.Customers.Customer>> GetAllAsync();
    Task UpdateAsync(SMBErp.Domain.Customers.Customer customer);
    Task DeleteAsync(int id);
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
