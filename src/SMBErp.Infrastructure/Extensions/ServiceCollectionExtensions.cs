using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMBErp.Application.Common.Interfaces;
using SMBErp.Infrastructure.Data;
using SMBErp.Infrastructure.Data.Repositories;

namespace SMBErp.Infrastructure.Extensions;

/// <summary>
/// Extension-Methoden für die Registrierung von Infrastructure-Services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registriert alle Infrastructure-Services
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Datenbank konfigurieren
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Repositories registrieren
        services.AddScoped(typeof(GenericRepository<>));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();

        return services;
    }
}
