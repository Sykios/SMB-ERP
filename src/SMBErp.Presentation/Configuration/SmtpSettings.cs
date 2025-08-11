namespace SMBErp.Presentation.Configuration;

/// <summary>
/// SMTP-Konfiguration für E-Mail-Versand
/// </summary>
public class SmtpSettings
{
    public const string SectionName = "SmtpSettings";

    /// <summary>
    /// SMTP-Server Adresse
    /// </summary>
    public string Server { get; set; } = string.Empty;

    /// <summary>
    /// SMTP-Port (standardmäßig 587 für TLS)
    /// </summary>
    public int Port { get; set; } = 587;

    /// <summary>
    /// SSL/TLS aktivieren
    /// </summary>
    public bool EnableSSL { get; set; } = true;

    /// <summary>
    /// Benutzername für SMTP-Authentifizierung
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Passwort für SMTP-Authentifizierung
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Absender E-Mail-Adresse
    /// </summary>
    public string FromEmail { get; set; } = string.Empty;

    /// <summary>
    /// Absender Name
    /// </summary>
    public string FromName { get; set; } = string.Empty;
}
