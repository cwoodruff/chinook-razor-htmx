using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Tracks;

public class EditModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Track Track { get; set; } = default!;
    
        


    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Track = await context.Tracks.FirstOrDefaultAsync(m => m.Id == id);

        if (Track == null)
        {
            return NotFound();
        }
        
        ViewData["AlbumId"] = new SelectList(context.Albums, "Id", "Id");
        ViewData["GenreId"] = new SelectList(context.Genres, "Id", "Id");
        ViewData["MediaTypeId"] = new SelectList(context.MediaTypes, "Id", "Id");

        if (Request.IsHtmx())
        {
            return Partial("EditModal", this);
        }

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Return validation errors as partial view
            return Partial("_ValidationErrors", ModelState);
        }

        context.Attach(Track).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"Track '{Track.Name}' updated successfully!");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TrackExists(Track.Id))
            {
                return Partial("_ErrorMessage", "Track not found. It may have been deleted by another user.");
            }
            else
            {
                return Partial("_ErrorMessage", "A concurrency error occurred. Please refresh and try again.");
            }
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error updating track: {ex.Message}");
        }
    }

    // Keep the modal method for backward compatibility
    public async Task<IActionResult> OnPostModalAsync()
    {
        return await OnPostAsync();
    }

    private bool TrackExists(int id)
    {
        return context.Tracks.Any(e => e.Id == id);
    }
}