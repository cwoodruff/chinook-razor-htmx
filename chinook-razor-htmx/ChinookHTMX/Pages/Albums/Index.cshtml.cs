using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Albums
{
    public class IndexModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public IndexModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public IList<Album> Album { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Album = await _context.Albums
                .Include(a => a.Artist).ToListAsync();
        }
    }
}