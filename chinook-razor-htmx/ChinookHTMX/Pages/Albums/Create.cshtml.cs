using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Albums;

public class CreateModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Album Album { get; set; } = default!;

    public IActionResult OnGet()
    {
        if (Request.IsHtmx())
        {
            return Partial("Albums/CreateModal", this);
        }

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Partial("_ValidationErrors", ModelState);
        }
        
        try
        {
            context.Albums.Add(Album);
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"Album '{Album.Title}' created successfully!");
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error creating album: {ex.Message}");
        }
    }
}