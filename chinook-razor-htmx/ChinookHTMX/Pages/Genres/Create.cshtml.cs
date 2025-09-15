using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Genres;

public class CreateModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Genre Genre { get; set; } = default!;

    public IActionResult OnGet()
    {
        if (Request.IsHtmx())
        {
            return Partial("Genres/CreateModal", this);
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
            context.Genres.Add(Genre);
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"Genre '{Genre.Name}' created successfully!");
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error creating genre: {ex.Message}");
        }
    }
}