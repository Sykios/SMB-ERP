using SMBErp.Domain.Customers;

namespace SMBErp.Application.Services;

/// <summary>
/// Application Service für Kunden-spezifische Geschäftsoperationen
/// </summary>
public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    /// <summary>
    /// Erstellt einen neuen Kunden mit Validierung
    /// </summary>
    public async Task<Customer> CreateCustomerAsync(CreateCustomerRequest request)
    {
        // Business Logic hier konzentrieren
        var customer = new Customer
        {
            CustomerNumber = await GenerateCustomerNumberAsync(),
            CompanyName = request.CompanyName,
            ContactFirstName = request.ContactFirstName,
            ContactLastName = request.ContactLastName,
            Email = request.Email,
            BillingStreet = request.BillingStreet,
            BillingZipCode = request.BillingZipCode,
            BillingCity = request.BillingCity
        };

        return await _customerRepository.AddAsync(customer);
    }

    private async Task<string> GenerateCustomerNumberAsync()
    {
        var lastCustomer = await _customerRepository.GetLastCustomerAsync();
        var lastNumber = lastCustomer?.CustomerNumber?.Substring(2);
        var nextNumber = int.Parse(lastNumber ?? "0") + 1;
        return $"KD{nextNumber:D6}";
    }
}

public record CreateCustomerRequest(
    string? CompanyName,
    string ContactFirstName,
    string ContactLastName,
    string? Email,
    string BillingStreet,
    string BillingZipCode,
    string BillingCity);

public interface ICustomerRepository
{
    Task<Customer> AddAsync(Customer customer);
    Task<Customer?> GetLastCustomerAsync();
}
