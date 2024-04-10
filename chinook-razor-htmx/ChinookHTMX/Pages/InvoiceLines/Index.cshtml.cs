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
    public class IndexModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public IndexModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public IList<InvoiceLine> InvoiceLine { get;set; } = default!;

        public async Task OnGetAsync()
        {
            InvoiceLine = await _context.InvoiceLines
                .Include(i => i.Invoice)
                .Include(i => i.Track).ToListAsync();
        }
    }
}
