# SMB ERP System

Ein modernes ERP-System für kleine und mittlere Unternehmen, entwickelt mit ASP.NET Core 8.0 und Razor Pages.

## Funktionen

### ✅ Implementierte Module
- **Benutzerverwaltung**: ASP.NET Identity mit Rollen (Admin, Benutzer)
- **Kundenverwaltung**: Vollständige CRUD-Operationen für Kundendaten
- **Rechnungserstellung**: Erstellen, bearbeiten und anzeigen von Rechnungen
- **PDF-Export**: Automatische PDF-Generierung mit QuestPDF
- **E-Mail-Versand**: Rechnungsversand via SMTP (konfigurierbar)
- **Produkte/Dienstleistungen**: Katalogverwaltung

### 🚀 Geplante Erweiterungen
- Lagerverwaltung
- Berichtswesen und Dashboards
- Buchhaltungsintegration
- API für Drittanbieter-Integration

## Technische Details

### Tech Stack
- **Framework**: ASP.NET Core 8.0 mit Razor Pages
- **Datenbank**: Entity Framework Core mit SQLite
- **Authentifizierung**: ASP.NET Core Identity
- **PDF-Generierung**: QuestPDF
- **E-Mail**: MailKit/SMTP
- **Frontend**: Bootstrap 5, HTML5, CSS3, JavaScript

### Systemanforderungen
- .NET 8 Runtime (kostenlos, ~60 MB)
- Windows 10/11 oder Windows Server
- Mindestens 4 GB RAM
- 1 GB freier Festplattenspeicher

## Installation und Deployment

### Lokales Development
```bash
# Repository klonen
git clone [repository-url]
cd SMB-ERP

# Abhängigkeiten installieren
dotnet restore

# Datenbank erstellen und migrieren
dotnet ef database update

# Anwendung starten
dotnet run
```

### Produktive Bereitstellung

#### Option 1: Einfaches Hosting
```bash
# Anwendung veröffentlichen
dotnet publish -c Release -o ./publish

# Anwendung starten
cd publish
dotnet SMBErp.dll
```

#### Option 2: IIS Deployment
1. IIS Features aktivieren
2. .NET 8 Hosting Bundle installieren
3. Anwendung in IIS konfigurieren
4. SSL-Zertifikat einrichten (empfohlen)

## Projektstruktur

```
SMB ERP/
├── Areas/                     # ASP.NET Identity UI
├── Configuration/             # Konfigurationsklassen
├── Controllers/               # API Controller (falls benötigt)
├── Data/                     # Entity Framework DbContext
├── Models/                   # Datenmodelle
├── Pages/                    # Razor Pages
│   ├── Customers/           # Kundenverwaltung
│   ├── Invoices/            # Rechnungsverwaltung
│   └── Products/            # Produktverwaltung
├── Services/                 # Business Logic Services
├── Utilities/                # Helper-Klassen
├── ViewModels/               # ViewModels für komplexe Views
└── wwwroot/                  # Statische Dateien
```

## Konfiguration

### E-Mail-Einstellungen (appsettings.json)
```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "ihr-unternehmen@example.com",
    "SenderName": "Ihr Unternehmen",
    "Username": "username",
    "Password": "password",
    "EnableSsl": true
  }
}
```

### Datenbank-Konfiguration
- **Development**: SQLite (app.db)
- **Production**: SQLite oder SQL Server Express

## Sicherheit

- HTTPS-Verschlüsselung
- ASP.NET Core Identity für Authentifizierung
- Rollenbasierte Autorisierung
- Input-Validierung und XSS-Schutz
- CSRF-Schutz für alle Formulare
- GDPR/DSGVO-konforme Datenverarbeitung

## Support und Dokumentation

### Logging

## Lizenz
