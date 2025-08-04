namespace SMBErp.Models.Enums;

/// <summary>
/// Status eines Kunden
/// </summary>
public enum CustomerStatus
{
    /// <summary>
    /// Aktiver Kunde
    /// </summary>
    Active = 1,

    /// <summary>
    /// Inaktiver Kunde
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Gesperrter Kunde
    /// </summary>
    Blocked = 3,

    /// <summary>
    /// Prospektiver Kunde
    /// </summary>
    Prospect = 4
}

/// <summary>
/// Status einer Rechnung
/// </summary>
public enum InvoiceStatus
{
    /// <summary>
    /// Entwurf - noch nicht versendet
    /// </summary>
    Draft = 1,

    /// <summary>
    /// Versendet - Rechnung wurde an den Kunden gesendet
    /// </summary>
    Sent = 2,

    /// <summary>
    /// Teilweise bezahlt
    /// </summary>
    PartiallyPaid = 3,

    /// <summary>
    /// Vollständig bezahlt
    /// </summary>
    Paid = 4,

    /// <summary>
    /// Überfällig - Zahlungsfrist überschritten
    /// </summary>
    Overdue = 5,

    /// <summary>
    /// Storniert
    /// </summary>
    Cancelled = 6
}

/// <summary>
/// Typ eines E-Mail-Templates
/// </summary>
public enum EmailTemplateType
{
    /// <summary>
    /// Rechnungsversand
    /// </summary>
    Invoice = 1,

    /// <summary>
    /// Zahlungserinnerung
    /// </summary>
    PaymentReminder = 2,

    /// <summary>
    /// Mahnung
    /// </summary>
    Dunning = 3,

    /// <summary>
    /// Angebot
    /// </summary>
    Quote = 4,

    /// <summary>
    /// Allgemeine Korrespondenz
    /// </summary>
    General = 5
}

/// <summary>
/// Einheit für Produkte und Dienstleistungen
/// </summary>
public enum Unit
{
    /// <summary>
    /// Stück
    /// </summary>
    Piece = 1,

    /// <summary>
    /// Kilogramm
    /// </summary>
    Kilogram = 2,

    /// <summary>
    /// Meter
    /// </summary>
    Meter = 3,

    /// <summary>
    /// Quadratmeter
    /// </summary>
    SquareMeter = 4,

    /// <summary>
    /// Kubikmeter
    /// </summary>
    CubicMeter = 5,

    /// <summary>
    /// Liter
    /// </summary>
    Liter = 6,

    /// <summary>
    /// Stunden (für Dienstleistungen)
    /// </summary>
    Hour = 7,

    /// <summary>
    /// Tage (für Dienstleistungen)
    /// </summary>
    Day = 8,

    /// <summary>
    /// Pauschal (für Dienstleistungen)
    /// </summary>
    Flat = 9
}
