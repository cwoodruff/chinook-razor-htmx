using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Tracks;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Track Track { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Track = await context.Tracks
            .Include(a => a.Album)
            .Include(a => a.MediaType)
            .Include(a => a.Genre)// Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Track == null)
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
            return Partial("_DeleteError", "Invalid track ID.");
        }

        Track = await context.Tracks
            .Include(a => a.Album)
            .Include(a => a.MediaType)
            .Include(a => a.Genre)// Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Track == null)
        {
            return Partial("_DeleteError", "Track not found. It may have been already deleted.");
        }

        try
        {
            var trackName = Track.Name;
            context.Tracks.Remove(Track);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"Track '{trackName}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting track: {ex.Message}");
        }
    }
}