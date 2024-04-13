using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Artists
{
    public class DeleteModal : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public DeleteModal(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        [BindProperty] public Artist Artist { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                Artist = artist;
                _context.Artists.Remove(Artist);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}