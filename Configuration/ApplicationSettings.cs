namespace SMBErp.Configuration;

/// <summary>
/// Allgemeine Anwendungseinstellungen
/// </summary>
public class ApplicationSettings
{
    public const string SectionName = "ApplicationSettings";

    /// <summary>
    /// Standard-Sprache der Anwendung
    /// </summary>
    public string DefaultLanguage { get; set; } = "de-DE";

    /// <summary>
    /// Datums-Format für Anzeige
    /// </summary>
    public string DateFormat { get; set; } = "dd.MM.yyyy";

    /// <summary>
    /// Zeit-Format für Anzeige
    /// </summary>
    public string TimeFormat { get; set; } = "HH:mm";

    /// <summary>
    /// Maximale Dateigröße für Uploads in MB
    /// </summary>
    public int MaxFileUploadSizeMB { get; set; } = 10;

    /// <summary>
    /// Session-Timeout in Minuten
    /// </summary>
    public int SessionTimeoutMinutes { get; set; } = 60;

    /// <summary>
    /// Maximale Anzahl Login-Versuche
    /// </summary>
    public int MaxLoginAttempts { get; set; } = 5;

    /// <summary>
    /// Lockout-Dauer in Minuten nach zu vielen fehlgeschlagenen Login-Versuchen
    /// </summary>
    public int LockoutDurationMinutes { get; set; } = 15;

    /// <summary>
    /// Aktiviert detaillierte Audit-Logs
    /// </summary>
    public bool EnableAuditLogs { get; set; } = true;

    /// <summary>
    /// Aktiviert Performance-Monitoring
    /// </summary>
    public bool EnablePerformanceMonitoring { get; set; } = false;
}
