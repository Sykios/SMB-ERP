using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SMBErp.Domain.Common.ValueObjects;

/// <summary>
/// Value Object für Geldbeträge mit Währungsunterstützung
/// </summary>
public readonly record struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency = "EUR")
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Währung darf nicht leer sein", nameof(currency));

        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency.ToUpperInvariant();
    }

    /// <summary>
    /// Erstellt einen neuen Money-Wert mit 0 Betrag
    /// </summary>
    public static Money Zero(string currency = "EUR") => new(0, currency);

    /// <summary>
    /// Prüft ob der Betrag negativ ist
    /// </summary>
    public bool IsNegative => Amount < 0;

    /// <summary>
    /// Prüft ob der Betrag positiv ist
    /// </summary>
    public bool IsPositive => Amount > 0;

    /// <summary>
    /// Prüft ob der Betrag null ist
    /// </summary>
    public bool IsZero => Amount == 0;

    /// <summary>
    /// Gibt den absoluten Betrag zurück
    /// </summary>
    public Money Abs() => new(Math.Abs(Amount), Currency);

    /// <summary>
    /// Negiert den Betrag
    /// </summary>
    public Money Negate() => new(-Amount, Currency);

    /// <summary>
    /// Addition von zwei Money-Objekten (gleiche Währung erforderlich)
    /// </summary>
    public static Money operator +(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    /// <summary>
    /// Subtraktion von zwei Money-Objekten (gleiche Währung erforderlich)
    /// </summary>
    public static Money operator -(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    /// <summary>
    /// Multiplikation mit einem Faktor
    /// </summary>
    public static Money operator *(Money money, decimal factor) => 
        new(money.Amount * factor, money.Currency);

    /// <summary>
    /// Multiplikation mit einem Faktor
    /// </summary>
    public static Money operator *(decimal factor, Money money) => money * factor;

    /// <summary>
    /// Division durch einen Divisor
    /// </summary>
    public static Money operator /(Money money, decimal divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Division durch Null ist nicht erlaubt");
        
        return new Money(money.Amount / divisor, money.Currency);
    }

    /// <summary>
    /// Vergleichsoperatoren
    /// </summary>
    public static bool operator >(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount > right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount < right.Amount;
    }

    public static bool operator >=(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount >= right.Amount;
    }

    public static bool operator <=(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount <= right.Amount;
    }

    /// <summary>
    /// Stellt sicher, dass beide Money-Objekte die gleiche Währung haben
    /// </summary>
    private static void EnsureSameCurrency(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException($"Operationen zwischen verschiedenen Währungen nicht erlaubt: {left.Currency} und {right.Currency}");
    }

    /// <summary>
    /// Formatiert den Geldbetrag für die Anzeige
    /// </summary>
    public override string ToString() => ToString("C");

    /// <summary>
    /// Formatiert den Geldbetrag mit einem spezifischen Format
    /// </summary>
    public string ToString(string format)
    {
        var culture = Currency switch
        {
            "EUR" => new CultureInfo("de-AT"),
            "USD" => new CultureInfo("en-US"),
            "CHF" => new CultureInfo("de-CH"),
            _ => CultureInfo.CurrentCulture
        };

        return format.ToUpperInvariant() switch
        {
            "C" or "CURRENCY" => Amount.ToString("C", culture),
            "N" or "NUMBER" => Amount.ToString("N2", culture),
            "F" or "FIXED" => Amount.ToString("F2", culture),
            _ => Amount.ToString(format, culture)
        };
    }

    /// <summary>
    /// Formatiert nur den Betrag ohne Währungssymbol
    /// </summary>
    public string ToAmountString() => ToString("F");

    /// <summary>
    /// Implizite Konvertierung von decimal zu Money (EUR)
    /// </summary>
    public static implicit operator Money(decimal amount) => new(amount);

    /// <summary>
    /// Explizite Konvertierung von Money zu decimal
    /// </summary>
    public static explicit operator decimal(Money money) => money.Amount;
}
