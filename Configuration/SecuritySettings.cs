namespace SMBErp.Configuration;

/// <summary>
/// Sicherheitseinstellungen für die Anwendung
/// </summary>
public class SecuritySettings
{
    public const string SectionName = "Security";

    /// <summary>
    /// HTTPS erzwingen
    /// </summary>
    public bool RequireHttps { get; set; } = true;

    /// <summary>
    /// HSTS Max-Age in Sekunden (1 Jahr = 31536000)
    /// </summary>
    public int HstsMaxAge { get; set; } = 31536000;

    /// <summary>
    /// Security Headers aktivieren
    /// </summary>
    public bool EnableSecurityHeaders { get; set; } = true;

    /// <summary>
    /// Cookie Secure Policy
    /// </summary>
    public string CookieSecurePolicy { get; set; } = "Always";

    /// <summary>
    /// Cookie SameSite Policy
    /// </summary>
    public string CookieSameSite { get; set; } = "Strict";

    /// <summary>
    /// Content Security Policy aktivieren
    /// </summary>
    public bool EnableCsp { get; set; } = true;

    /// <summary>
    /// X-Frame-Options Header aktivieren
    /// </summary>
    public bool EnableXFrameOptions { get; set; } = true;

    /// <summary>
    /// X-Content-Type-Options Header aktivieren
    /// </summary>
    public bool EnableXContentTypeOptions { get; set; } = true;

    /// <summary>
    /// API Rate Limiting aktivieren
    /// </summary>
    public bool EnableRateLimiting { get; set; } = true;

    /// <summary>
    /// Maximale Requests pro Minute pro IP
    /// </summary>
    public int MaxRequestsPerMinute { get; set; } = 100;
}
