using System.Text.RegularExpressions;

namespace SMBErp.Domain.Common.ValueObjects;

/// <summary>
/// Value Object für E-Mail-Adressen mit Validierung
/// </summary>
public readonly record struct EmailAddress
{
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; }

    public EmailAddress(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-Mail-Adresse darf nicht leer sein", nameof(email));

        var trimmedEmail = email.Trim().ToLowerInvariant();
        
        if (!EmailRegex.IsMatch(trimmedEmail))
            throw new ArgumentException($"Ungültige E-Mail-Adresse: {email}", nameof(email));

        Value = trimmedEmail;
    }

    /// <summary>
    /// Gibt den Benutzernamen (Teil vor @) zurück
    /// </summary>
    public string UserName => Value.Split('@')[0];

    /// <summary>
    /// Gibt die Domäne (Teil nach @) zurück
    /// </summary>
    public string Domain => Value.Split('@')[1];

    /// <summary>
    /// Prüft ob es sich um eine geschäftliche E-Mail-Adresse handelt
    /// </summary>
    public bool IsBusinessEmail => !IsPersonalEmail;

    /// <summary>
    /// Prüft ob es sich um eine private E-Mail-Adresse handelt
    /// </summary>
    public bool IsPersonalEmail
    {
        get
        {
            var personalDomains = new[]
            {
                "gmail.com", "yahoo.com", "hotmail.com", "outlook.com",
                "gmx.at", "gmx.de", "web.de", "t-online.de",
                "aon.at", "chello.at", "icloud.com", "me.com"
            };

            return personalDomains.Contains(Domain);
        }
    }

    public override string ToString() => Value;

    /// <summary>
    /// Implizite Konvertierung von string zu EmailAddress
    /// </summary>
    public static implicit operator EmailAddress(string email) => new(email);

    /// <summary>
    /// Implizite Konvertierung von EmailAddress zu string
    /// </summary>
    public static implicit operator string(EmailAddress email) => email.Value;

    /// <summary>
    /// Versucht eine E-Mail-Adresse zu parsen ohne Exception zu werfen
    /// </summary>
    public static bool TryParse(string? email, out EmailAddress emailAddress)
    {
        emailAddress = default;
        
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            emailAddress = new EmailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
