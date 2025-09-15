using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.MediaTypes;

public class EditModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public MediaType MediaType { get; set; } = default!;

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        MediaType = await context.MediaTypes.FirstOrDefaultAsync(m => m.Id == id);

        if (MediaType == null)
        {
            return NotFound();
        }

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

        context.Attach(MediaType).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"MediaType '{MediaType.Name}' updated successfully!");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MediaTypeExists(MediaType.Id))
            {
                return Partial("_ErrorMessage", "MediaType not found. It may have been deleted by another user.");
            }
            else
            {
                return Partial("_ErrorMessage", "A concurrency error occurred. Please refresh and try again.");
            }
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error updating media type: {ex.Message}");
        }
    }

    // Keep the modal method for backward compatibility
    public async Task<IActionResult> OnPostModalAsync()
    {
        return await OnPostAsync();
    }

    private bool MediaTypeExists(int id)
    {
        return context.MediaTypes.Any(e => e.Id == id);
    }
}