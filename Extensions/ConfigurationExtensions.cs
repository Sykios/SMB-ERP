using Microsoft.Extensions.Options;
using SMBErp.Configuration;

namespace SMBErp.Extensions;

/// <summary>
/// Extension-Klasse für die Konfiguration der Anwendung
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Registriert alle Konfigurationsklassen als Optionen
    /// </summary>
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // SMTP-Einstellungen
        services.Configure<SmtpSettings>(
            configuration.GetSection(SmtpSettings.SectionName));
        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<SmtpSettings>>().Value);

        // Firmen-Einstellungen
        services.Configure<CompanySettings>(
            configuration.GetSection(CompanySettings.SectionName));
        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<CompanySettings>>().Value);

        // Rechnungs-Einstellungen
        services.Configure<InvoiceSettings>(
            configuration.GetSection(InvoiceSettings.SectionName));
        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<InvoiceSettings>>().Value);

        // Anwendungs-Einstellungen
        services.Configure<ApplicationSettings>(
            configuration.GetSection(ApplicationSettings.SectionName));
        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<ApplicationSettings>>().Value);

        // Sicherheits-Einstellungen
        services.Configure<SecuritySettings>(
            configuration.GetSection(SecuritySettings.SectionName));
        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<SecuritySettings>>().Value);

        return services;
    }
}
