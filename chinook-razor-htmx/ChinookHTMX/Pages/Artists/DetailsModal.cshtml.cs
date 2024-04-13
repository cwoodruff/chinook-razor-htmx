using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Artists
{
    public class DetailsModal : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public DetailsModal(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public Artist Artist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            else
            {
                Artist = artist;
            }

            return Page();
        }
    }
}