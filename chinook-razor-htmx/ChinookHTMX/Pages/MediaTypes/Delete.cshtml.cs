using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.MediaTypes;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public MediaType MediaType { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        MediaType = await context.MediaTypes
            .Include(a => a.Tracks) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (MediaType == null)
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
            return Partial("_DeleteError", "Invalid media type ID.");
        }

        MediaType = await context.MediaTypes
            .Include(a => a.Tracks) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (MediaType == null)
        {
            return Partial("_DeleteError", "MediaType not found. It may have been already deleted.");
        }

        try
        {
            // Check for related records
            if (MediaType.Tracks.Any())
            {
                return Partial("_DeleteError",
                    $"Cannot delete media type '{MediaType.Name}' because it has {MediaType.Tracks.Count} associated tracks(s). Please reassign the tracks first.");
            }

            var mediatypeName = MediaType.Name;
            context.MediaTypes.Remove(MediaType);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"MediaType '{mediatypeName}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting media type: {ex.Message}");
        }
    }
}