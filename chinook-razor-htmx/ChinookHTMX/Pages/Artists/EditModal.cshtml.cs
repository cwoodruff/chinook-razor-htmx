using ChinookHTMX.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChinookHTMX.Pages.Artists;

public class EditModal(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    [BindProperty] public Artist? Artist { get; set; }

    public async Task OnGetAsync(int id)
    {
        Artist = await context.Artists.FindAsync(id);
    }
    
    public async Task<IActionResult> OnPostModalEdit()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Attach(Artist).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArtistExists(Artist.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }
    
    private bool ArtistExists(int id)
    {
        return context.Albums.Any(e => e.Id == id);
    }
}