using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Albums;

public class DeleteModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    [BindProperty] public Album Album { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var album = await context.Albums.FirstOrDefaultAsync(m => m.Id == id);

        if (album == null)
        {
            return NotFound();
        }
        else
        {
            Album = album;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var album = await context.Albums.FindAsync(id);
        if (album != null)
        {
            Album = album;
            context.Albums.Remove(Album);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}