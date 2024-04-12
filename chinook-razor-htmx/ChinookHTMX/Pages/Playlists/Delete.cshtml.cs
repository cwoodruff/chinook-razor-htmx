using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Data;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Playlists
{
    public class DeleteModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public DeleteModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        [BindProperty] public Playlist Playlist { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                Playlist = playlist;
                _context.Playlists.Remove(Playlist);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}