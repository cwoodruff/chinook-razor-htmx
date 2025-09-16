using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Genres;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Genre Genre { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Genre = await context.Genres
            .Include(a => a.Tracks) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Genre == null)
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
            return Partial("_DeleteError", "Invalid Genre ID.");
        }

        Genre = await context.Genres
            .Include(a => a.Tracks) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Genre == null)
        {
            return Partial("_DeleteError", "Genre not found. It may have been already deleted.");
        }

        try
        {
            // Check for related records
            if (Genre.Tracks.Any())
            {
                return Partial("_DeleteError",
                    $"Cannot delete genre '{Genre.Name}' because it has {Genre.Tracks.Count} associated track(s). Please reassign the tracks first.");
            }

            var genreName = Genre.Name;
            context.Genres.Remove(Genre);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"Genre '{genreName}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting genre: {ex.Message}");
        }
    }
}