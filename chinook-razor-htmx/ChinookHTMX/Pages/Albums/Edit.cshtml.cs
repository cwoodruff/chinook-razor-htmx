using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChinookHTMX.Pages.Albums;

public class EditModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Album Album { get; set; } = default!;

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Album = await context.Albums.FirstOrDefaultAsync(m => m.Id == id);

        if (Album == null)
        {
            return NotFound();
        }
        
        ViewData["ArtistId"] = new SelectList(context.Artists, "Id", "Id");

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

        context.Attach(Album).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"Album '{Album.Title}' updated successfully!");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AlbumExists(Album.Id))
            {
                return Partial("_ErrorMessage", "Album not found. It may have been deleted by another user.");
            }
            else
            {
                return Partial("_ErrorMessage", "A concurrency error occurred. Please refresh and try again.");
            }
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error updating album: {ex.Message}");
        }
    }

    // Keep the modal method for backward compatibility
    public async Task<IActionResult> OnPostModalAsync()
    {
        return await OnPostAsync();
    }

    private bool AlbumExists(int id)
    {
        return context.Albums.Any(e => e.Id == id);
    }
}