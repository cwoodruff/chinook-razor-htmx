using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Playlists
{
    public class DetailsModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public DetailsModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public Playlist Playlist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists.FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }
            else
            {
                Playlist = playlist;
            }

            return Page();
        }
    }
}