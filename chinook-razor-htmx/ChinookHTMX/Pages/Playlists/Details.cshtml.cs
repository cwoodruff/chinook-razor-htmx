using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Playlists;

public class DetailsModel(Data.ChinookContext context) : PageModel
{
    public Playlist Playlist { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var playlist = await context.Playlists.FirstOrDefaultAsync(m => m.Id == id);
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