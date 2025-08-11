# SMB ERP System

Ein modernes ERP-System für kleine und mittelständische Unternehmen, entwickelt mit Clean Architecture und Domain-Driven Design Prinzipien.

## 📁 Projektstruktur

Das Projekt folgt der **Clean Architecture** mit einer klaren Trennung der Verantwortlichkeiten:

```
SMB-ERP/
├── src/                           # Hauptquellcode
│   ├── SMBErp.Domain/            # Domain Layer (Geschäftslogik)
│   ├── SMBErp.Application/       # Application Layer (Use Cases)
│   ├── SMBErp.Infrastructure/    # Infrastructure Layer (Daten, Services)
│   └── SMBErp.Presentation/              # Presentation Layer
├── tests/                        # Unit- und Integrationstests
│   ├── SMBErp.Domain.Tests/
│   ├── SMBErp.Application.Tests/
│   ├── SMBErp.Infrastructure.Tests/
│   └── SMBErp.Web.Tests/
├── docs/                         # Dokumentation
├── scripts/                      # Build- und Deployment-Skripte
└── SMBErp.sln                   # Solution-Datei
```

## 🏗️ Architektur-Schichten

### 1. Domain Layer (`SMBErp.Domain`)
**Zweck**: Enthält die Geschäftslogik und Domain-Modelle
- **Abhängigkeiten**: Keine (reiner .NET Core)
- **Verantwortung**: Entitäten, Value Objects, Domain Services, Geschäftsregeln

```
Domain/
├── Common/                       # Gemeinsame Domain-Komponenten
│   ├── BaseEntity.cs            # Basis-Entität mit Audit-Feldern
│   └── ValueObjects/            # Value Objects (Money, EmailAddress)
├── Customers/                   # Kunden-Domain
│   └── Customer.cs              # Kunden-Entität
├── Sales/                       # Verkaufs-Domain
│   ├── Invoice.cs               # Rechnungs-Entität
│   └── InvoiceItem.cs          # Rechnungsposition
├── Inventory/                   # Inventar-Domain
│   ├── Item.cs                  # Basis-Artikel
│   ├── Product.cs               # Physisches Produkt
│   └── Service.cs               # Dienstleistung
├── Company/                     # Unternehmens-Domain
└── Shared/                      # Geteilte Enums und Konstanten
    └── Enums.cs
```

### 2. Application Layer (`SMBErp.Application`)
**Zweck**: Orchestriert Domain-Objekte für spezifische Anwendungsfälle
- **Abhängigkeiten**: Domain Layer
- **Verantwortung**: Use Cases, Application Services, DTOs, Interfaces

### 3. Infrastructure Layer (`SMBErp.Infrastructure`)
**Zweck**: Implementiert externe Abhängigkeiten und technische Services
- **Abhängigkeiten**: Domain, Application Layer
- **Verantwortung**: Datenbankzugriff, externe APIs, E-Mail, Dateisystem

### 4. Presentation Layer (`SMBErp.Presentation`)
**Zweck**: Web-Interface und API-Endpoints
- **Abhängigkeiten**: Alle anderen Layer
- **Verantwortung**: Controllers, Views, ViewModels, API

## 🛠️ Technologien

- **.NET 8**: Moderne .NET-Plattform
- **Entity Framework Core**: ORM für Datenbankzugriff
- **SQLite**: Lokale Datenbank (produktionsbereit für kleine Teams)
- **ASP.NET Core Razor Pages**: Web-Framework
- **Identity**: Authentifizierung und Autorisierung
- **Serilog**: Strukturiertes Logging
- **QuestPDF**: PDF-Generierung für Rechnungen
- **MailKit**: E-Mail-Versand
- **xUnit**: Unit Testing Framework
- **FluentAssertions**: Assertions für Tests

## 🚀 Quick Start

### Voraussetzungen
- .NET 8 SDK
- Visual Studio 2022 oder Visual Studio Code

### Projekt starten
```bash
# Repository klonen
git clone https://github.com/Sykios/SMB-ERP.git
cd SMB-ERP

# Abhängigkeiten wiederherstellen
dotnet restore

# Datenbank erstellen/migrieren
cd src/SMBErp.Presentation
dotnet ef database update

# Entwicklungsserver starten
dotnet run
```

### Tests ausführen
```bash
# Alle Tests ausführen
dotnet test

# Bestimmtes Testprojekt ausführen
dotnet test tests/SMBErp.Domain.Tests/
```

## 📋 Funktionen

### 🔄 Hauptfunktionalitäten
- **Kundenverwaltung**: Vollständige Kundenstammdaten
- **Artikelverwaltung**: Produkte und Dienstleistungen
- **Rechnungswesen**: Rechnungserstellung und -verwaltung
- **Benutzerauthentifizierung**: ASP.NET Core Identity
- **Audit-Trail**: Automatische Erstellung/Änderung-Protokollierung
- **Clean Architecture**: Saubere Trennung der Verantwortlichkeiten

### 🔄 In Planung
- **Dashboard**: Übersichts-Dashboard
- **Berichte**: Umsatz- und Kundenberichte
- **PDF-Export**: Automatische Rechnungs-PDFs
- **E-Mail-Integration**: Automatischer Rechnungsversand

## 🧪 Testing-Strategie

### Unit Tests
- **Domain Tests**: Geschäftslogik-Validierung
- **Application Tests**: Use Case-Validierung
- **Infrastructure Tests**: Repository-Tests mit In-Memory-DB

### Integration Tests
- **Web Tests**: End-to-End Controller-Tests
- **Database Tests**: Echte Datenbankintegration

## 🏛️ Domain-Driven Design Prinzipien

### Entitäten
Alle Entitäten, bis auf Company, erben von `BaseEntity` und enthalten:
- **Audit-Eigenschaften**: CreatedAt, UpdatedAt, CreatedBy, etc.
- **Soft Delete**: IsDeleted, DeletedAt, DeletedBy
- **Geschäftslogik-Methoden**: Domain-spezifische Operationen

### Value Objects
- **Money**: Währungsunterstützung mit Validierung
- **EmailAddress**: Validierte E-Mail-Adressen
- Immutable und validierte Wertobjekte

### Geschäftsregeln
- Werden in den Domain-Entitäten implementiert
- Keine Geschäftslogik in anderen Schichten
- Domain Services für komplexe Geschäftsregeln
