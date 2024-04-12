using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChinookHTMX.Data;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.InvoiceLines
{
    public class CreateModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public CreateModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id");
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id");
            return Page();
        }

        [BindProperty] public InvoiceLine InvoiceLine { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InvoiceLines.Add(InvoiceLine);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}