using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Albums;

public class CreateModel(Data.ChinookContext context) : PageModel
{
    public IActionResult OnGet()
    {
        ViewData["ArtistId"] = new SelectList(context.Artists, "Id", "Id");
        return Page();
    }

    [BindProperty] public Album Album { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Albums.Add(Album);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}