using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

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

        Playlist = await context.Playlists
            .Include(a => a.Tracks) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Playlist == null)
        {
            return NotFound();
        }

        // Return modal for HTMX requests
        if (Request.IsHtmx())
        {
            return Partial("DeleteModal", this);
        }

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return Partial("_DeleteError", "Invalid artist ID.");
        }

        Playlist = await context.Playlists
            .Include(a => a.Tracks)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Playlist == null)
        {
            return Partial("_DeleteError", "Playlist not found. It may have been already deleted.");
        }

        try
        {
            // Check for related records
            if (Playlist.Tracks.Any())
            {
                return Partial("_DeleteError",
                    $"Cannot delete playlist '{Playlist.Name}' because it has {Playlist.Tracks.Count} associated tracks(s). Please remove the tracks first.");
            }

            var playlistName = Playlist.Name;
            context.Playlists.Remove(Playlist);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"Playlist '{playlistName}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting playlist: {ex.Message}");
        }
    }
}