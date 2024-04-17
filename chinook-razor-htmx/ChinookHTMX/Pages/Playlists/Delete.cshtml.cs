using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Playlists;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Playlist Playlist { get; set; } = default!;

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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var playlist = await context.Playlists.FindAsync(id);
        if (playlist != null)
        {
            Playlist = playlist;
            context.Playlists.Remove(Playlist);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}