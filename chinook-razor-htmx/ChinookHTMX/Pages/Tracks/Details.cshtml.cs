using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Tracks
{
    public class DetailsModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public DetailsModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public Track Track { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks.FirstOrDefaultAsync(m => m.Id == id);
            if (track == null)
            {
                return NotFound();
            }
            else
            {
                Track = track;
            }

            return Page();
        }
    }
}