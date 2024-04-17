using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Genres;

public class DeleteModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    [BindProperty] public Genre Genre { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await context.Genres.FirstOrDefaultAsync(m => m.Id == id);

        if (genre == null)
        {
            return NotFound();
        }
        else
        {
            Genre = genre;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await context.Genres.FindAsync(id);
        if (genre != null)
        {
            Genre = genre;
            context.Genres.Remove(Genre);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}