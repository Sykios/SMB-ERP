namespace SMBErp.Configuration;

/// <summary>
/// Firmen-Konfiguration für Rechnungen und Dokumente
/// </summary>
public class CompanySettings
{
    public const string SectionName = "CompanySettings";

    /// <summary>
    /// Firmenname
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Straße und Hausnummer
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Postleitzahl
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Stadt
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Land
    /// </summary>
    public string Country { get; set; } = "Deutschland";

    /// <summary>
    /// Telefonnummer
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// E-Mail-Adresse
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Website
    /// </summary>
    public string Website { get; set; } = string.Empty;

    /// <summary>
    /// Umsatzsteuer-Identifikationsnummer
    /// </summary>
    public string VatId { get; set; } = string.Empty;

    /// <summary>
    /// Steuernummer
    /// </summary>
    public string TaxNumber { get; set; } = string.Empty;

    /// <summary>
    /// Handelsregistereintrag
    /// </summary>
    public string CommercialRegister { get; set; } = string.Empty;

    /// <summary>
    /// Bankname
    /// </summary>
    public string BankName { get; set; } = string.Empty;

    /// <summary>
    /// IBAN
    /// </summary>
    public string IBAN { get; set; } = string.Empty;

    /// <summary>
    /// BIC/SWIFT-Code
    /// </summary>
    public string BIC { get; set; } = string.Empty;

    /// <summary>
    /// Vollständige Adresse als formatierter String
    /// </summary>
    public string FullAddress => $"{Street}\n{ZipCode} {City}\n{Country}";
}
