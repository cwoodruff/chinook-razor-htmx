using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.InvoiceLines;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public InvoiceLine InvoiceLine { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        InvoiceLine = await context.InvoiceLines
            .Include(a => a.Invoice) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (InvoiceLine == null)
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
            return Partial("_DeleteError", "Invalid invoice line ID.");
        }

        InvoiceLine = await context.InvoiceLines
            .Include(a => a.Invoice) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (InvoiceLine == null)
        {
            return Partial("_DeleteError", "InvoiceLine not found. It may have been already deleted.");
        }

        try
        {
            // Check for related records
            if (InvoiceLine.InvoiceId.HasValue)
            {
                return Partial("_DeleteError",
                    $"Cannot delete invoice line '{InvoiceLine.Id}' because it has {InvoiceLine.Invoice.Id} associated invoice. Please delete the invoice first.");
            }

            var invoiveLineId = InvoiceLine.Id;
            context.InvoiceLines.Remove(InvoiceLine);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"InvoiceLine '{invoiveLineId}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting invoice line: {ex.Message}");
        }
    }
}