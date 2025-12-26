using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Invoices;

public class DetailsModel(Data.ChinookContext context) : PageModel
{
    public Invoice Invoice { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Invoice = await context.Invoices
            .Include(i => i.InvoiceLines)
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Invoice == null)
        {
            return NotFound();
        }

        if (Request.IsHtmx())
        {
            return Partial("DetailsModal", this);
        }

        return Page();
    }
}