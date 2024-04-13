using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.InvoiceLines
{
    public class DetailsModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public DetailsModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public InvoiceLine InvoiceLine { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceline = await _context.InvoiceLines.FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceline == null)
            {
                return NotFound();
            }
            else
            {
                InvoiceLine = invoiceline;
            }

            return Page();
        }
    }
}