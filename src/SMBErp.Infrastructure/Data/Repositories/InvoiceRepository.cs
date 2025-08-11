using Microsoft.EntityFrameworkCore;
using SMBErp.Application.Common.Interfaces;
using SMBErp.Domain.Sales;

namespace SMBErp.Infrastructure.Data.Repositories;

/// <summary>
/// Repository für Rechnungsverwaltung
/// </summary>
public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Rechnungen eines bestimmten Kunden abrufen
    /// </summary>
    public async Task<List<Invoice>> GetByCustomerAsync(int customerId)
    {
        return await _dbSet
            .Where(i => !i.IsDeleted && i.CustomerId == customerId)
            .Include(i => i.Customer)
            .Include(i => i.InvoiceItems.Where(ii => !ii.IsDeleted))
            .OrderByDescending(i => i.InvoiceDate)
            .ToListAsync();
    }

    /// <summary>
    /// Nächste verfügbare Rechnungsnummer generieren
    /// Format: RG-YYYY-NNNN
    /// </summary>
    public async Task<string> GetNextInvoiceNumberAsync()
    {
        var currentYear = DateTime.Now.Year;
        var yearPrefix = $"RG-{currentYear}-";

        // Letzte Rechnungsnummer für das aktuelle Jahr finden
        var lastInvoice = await _dbSet
            .Where(i => i.InvoiceNumber.StartsWith(yearPrefix))
            .OrderByDescending(i => i.InvoiceNumber)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (lastInvoice != null)
        {
            // Nummer aus der letzten Rechnung extrahieren
            var lastNumberPart = lastInvoice.InvoiceNumber.Substring(yearPrefix.Length);
            if (int.TryParse(lastNumberPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }

        return $"{yearPrefix}{nextNumber:D4}"; // 4-stellige Nummer mit führenden Nullen
    }
}
