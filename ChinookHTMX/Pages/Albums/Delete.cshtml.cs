using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Albums;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Album Album { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Album = await context.Albums
            .Include(a => a.Artist) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Album == null)
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
            return Partial("_DeleteError", "Invalid album ID.");
        }

        Album = await context.Albums
            .Include(a => a.ArtistId == Album.ArtistId) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Album == null)
        {
            return Partial("_DeleteError", "Album not found. It may have been already deleted.");
        }

        try
        {
            var albumTitle = Album.Title;
            context.Albums.Remove(Album);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"Album '{albumTitle}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting album: {ex.Message}");
        }
    }
}