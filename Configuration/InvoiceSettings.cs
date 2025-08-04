namespace SMBErp.Configuration;

/// <summary>
/// Rechnungs-spezifische Konfiguration
/// </summary>
public class InvoiceSettings
{
    public const string SectionName = "InvoiceSettings";

    /// <summary>
    /// Präfix für Rechnungsnummern (z.B. "RG")
    /// </summary>
    public string NumberPrefix { get; set; } = "RG";

    /// <summary>
    /// Startnummer für Rechnungen
    /// </summary>
    public int StartNumber { get; set; } = 1;

    /// <summary>
    /// Jährliche Zurücksetzung der Rechnungsnummer
    /// </summary>
    public bool YearlyReset { get; set; } = true;

    /// <summary>
    /// Standard-Zahlungsfrist in Tagen
    /// </summary>
    public int DefaultPaymentTermDays { get; set; } = 14;

    /// <summary>
    /// Standard-Mehrwertsteuersatz (20% in Österreich)
    /// </summary>
    public decimal DefaultVatRate { get; set; } = 20.0m;

    /// <summary>
    /// Währung (ISO-Code)
    /// </summary>
    public string Currency { get; set; } = "EUR";

    /// <summary>
    /// Währungssymbol
    /// </summary>
    public string CurrencySymbol { get; set; } = "€";
}
