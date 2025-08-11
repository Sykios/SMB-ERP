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
            throw new ArgumentException("Email cannot be empty", nameof(email));

        email = email.Trim().ToLowerInvariant();

        if (!EmailRegex.IsMatch(email))
            throw new ArgumentException($"Invalid email format: {email}", nameof(email));

        Value = email;
    }

    public static implicit operator string(EmailAddress email) => email.Value;
    public static explicit operator EmailAddress(string email) => new(email);

    public override string ToString() => Value;
}
