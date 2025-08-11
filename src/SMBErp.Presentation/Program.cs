using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SMBErp.Infrastructure.Data;
using SMBErp.Infrastructure.Extensions;
using SMBErp.Presentation.Extensions;
using SMBErp.Presentation.Middleware;

// Logger konfigurieren
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/smberp-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateBootstrapLogger();

try
{
    Log.Information("SMB ERP-Anwendung wird gestartet");

    var builder = WebApplication.CreateBuilder(args);
    
    // Explicitly configure Kestrel to use port 5001 (HTTPS)
    builder.WebHost.UseUrls("https://localhost:5001");
    
    Log.Information("WebApplication.Builder erstellt");

    // Serilog als Logger verwenden
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());
        
    Log.Information("Serilog konfiguriert");

    // Konfiguration registrieren
    builder.Services.AddAppConfiguration(builder.Configuration);
    Log.Information("App-Konfiguration hinzugefügt");

    // Infrastructure Services registrieren (inkl. EF Core und Repositories)
    builder.Services.AddInfrastructureServices(builder.Configuration);
    Log.Information("Infrastructure Services hinzugefügt");
    
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    Log.Information("Database Developer Page Exception Filter hinzugefügt");

    // Identity konfigurieren
    builder.Services.AddDefaultIdentity<IdentityUser>(options => {
        options.SignIn.RequireConfirmedAccount = false; // Temporarily disable for development
        options.Password.RequiredLength = 6;  // Make it easier for development
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

    // Razor Pages und Localization
    builder.Services.AddRazorPages();
    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

    // Session und Caching
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(60);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

    // Response Compression und Caching
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
    });
    builder.Services.AddResponseCaching();

    // Health Checks
    builder.Services.AddHealthChecks()
        .AddDbContextCheck<ApplicationDbContext>();

    var app = builder.Build();

    // Globale Exception Middleware
    app.UseMiddleware<GlobalErrorHandlerMiddleware>();

    // HTTP Pipeline konfigurieren
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    // Security Headers
    app.UseMiddleware<SecurityHeadersMiddleware>();

    // Standard Middleware
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseResponseCompression();
    app.UseResponseCaching();
    app.UseSession();

    // Localization
    var supportedCultures = new[] { "de-DE", "en-US" };
    var localizationOptions = new RequestLocalizationOptions()
        .SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
    app.UseRequestLocalization(localizationOptions);

    // Authentication und Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    // Endpoint Routing
    app.MapRazorPages();
    app.MapHealthChecks("/health");

    // Log application URLs
    app.Lifetime.ApplicationStarted.Register(() =>
    {
        Console.WriteLine("=".PadRight(60, '='));
        Console.WriteLine("SMB ERP Application started successfully!");
        Console.WriteLine("Opening browser at http://localhost:5001...");
        Console.WriteLine("=".PadRight(60, '='));
        
        // Browser automatisch öffnen (nur in Development) - SIMPLE VERSION
        if (app.Environment.IsDevelopment())
        {
            _ = Task.Run(async () =>
            {
                // Einfach 5 Sekunden warten, dann Browser öffnen
                await Task.Delay(5000);
                try
                {
                    var url = "https://localhost:5001";
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                    Console.WriteLine("Browser opened after 5 seconds delay!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not auto-open browser: {ex.Message}");
                }
            });
        }
    });

    // App starten
    Log.Information("Starting SMB ERP application on http://localhost:5001");
    Console.WriteLine("About to call app.Run()...");
    
    try
    {
        app.Run();
        Console.WriteLine("app.Run() completed normally.");
    }
    catch (Exception runEx)
    {
        Console.WriteLine($"Exception during app.Run(): {runEx.Message}");
        Console.WriteLine($"Stack Trace: {runEx.StackTrace}");
        throw;
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Die Anwendung wurde aufgrund eines Fehlers beendet");
    Console.WriteLine($"FATAL ERROR: {ex.Message}");
    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
    return 1; // Exit with error code
}
finally
{
    Log.CloseAndFlush();
}

return 0; // Success
