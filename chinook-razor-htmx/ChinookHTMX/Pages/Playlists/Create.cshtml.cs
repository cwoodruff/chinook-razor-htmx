using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Playlists;

public class CreateModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty] public Playlist Playlist { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Playlists.Add(Playlist);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}