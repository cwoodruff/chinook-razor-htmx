using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Data;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.InvoiceLines
{
    public class DeleteModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public DeleteModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceline = await _context.InvoiceLines.FindAsync(id);
            if (invoiceline != null)
            {
                InvoiceLine = invoiceline;
                _context.InvoiceLines.Remove(InvoiceLine);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
