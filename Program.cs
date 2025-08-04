using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SMBErp.Configuration;
using SMBErp.Data;
using SMBErp.Extensions;
using SMBErp.Middleware;
using System.Globalization;

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

    // Serilog als Logger verwenden
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    // Konfiguration registrieren
    builder.Services.AddAppConfiguration(builder.Configuration);

    // EF Core und Datenbank konfigurieren
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                          throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString, 
        sqliteOptions => sqliteOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    // Identity konfigurieren
    builder.Services.AddDefaultIdentity<IdentityUser>(options => {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequiredLength = 10;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
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

    // App starten
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Die Anwendung wurde aufgrund eines Fehlers beendet");
}
finally
{
    Log.CloseAndFlush();
}
