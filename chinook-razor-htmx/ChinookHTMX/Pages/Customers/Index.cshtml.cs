using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public IndexModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Customer = await _context.Customers
                .Include(c => c.SupportRep).ToListAsync();
        }
    }
}