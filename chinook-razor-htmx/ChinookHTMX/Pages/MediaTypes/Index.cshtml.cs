using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.MediaTypes
{
    public class IndexModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public IndexModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public IList<MediaType> MediaType { get; set; } = default!;

        public async Task OnGetAsync()
        {
            MediaType = await _context.MediaTypes.ToListAsync();
        }
    }
}