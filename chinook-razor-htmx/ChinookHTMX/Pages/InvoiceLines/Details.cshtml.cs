using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.InvoiceLines;

public class DetailsModel(Data.ChinookContext context) : PageModel
{
    public InvoiceLine InvoiceLine { get; set; } = default!;
    
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        InvoiceLine = await context.InvoiceLines.FirstOrDefaultAsync(m => m.Id == id);

        if (InvoiceLine == null)
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