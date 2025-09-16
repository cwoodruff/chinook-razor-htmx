using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Artists;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Artist Artist { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Artist = await context.Artists
            .Include(a => a.Albums) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Artist == null)
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

        Artist = await context.Artists
            .Include(a => a.Albums)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Artist == null)
        {
            return Partial("_DeleteError", "Artist not found. It may have been already deleted.");
        }

        try
        {
            // Check for related records
            if (Artist.Albums.Any())
            {
                return Partial("_DeleteError",
                    $"Cannot delete artist '{Artist.Name}' because it has {Artist.Albums.Count} associated album(s). Please delete the albums first.");
            }

            var artistName = Artist.Name;
            context.Artists.Remove(Artist);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"Artist '{artistName}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting artist: {ex.Message}");
        }
    }
}