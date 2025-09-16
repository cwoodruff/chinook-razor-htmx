using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Invoices;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Invoice Invoice { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Invoice = await context.Invoices
            .Include(i => i.InvoiceLines) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Invoice == null)
        {
            return NotFound();
        }

        // Return modal for HTMX requests
        if (Request.IsHtmx())
        {
            return Partial("DeleteModal", this);
        }

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return Partial("_DeleteError", "Invalid invoice ID.");
        }

        Invoice = await context.Invoices
            .Include(i => i.InvoiceLines)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Invoice == null)
        {
            return Partial("_DeleteError", "Invoice not found. It may have been already deleted.");
        }

        try
        {
            // Check for related records
            if (Invoice.InvoiceLines.Any())
            {
                return Partial("_DeleteError",
                    $"Cannot delete invoice '{Invoice.Id}' because it has {Invoice.InvoiceLines.Count} associated invoice lines(s). Please delete the invoice lines first.");
            }

            var invoiceName = Invoice.Id;
            context.Invoices.Remove(Invoice);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"Invoice '{invoiceName}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting invoice: {ex.Message}");
        }
    }
}