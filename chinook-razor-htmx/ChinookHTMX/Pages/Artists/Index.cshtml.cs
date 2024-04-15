using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Artists;

public class IndexModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    public IList<Artist> Artists { get; set; } = default!;
    public Artist Artist { get; set; }

    public async Task OnGetAsync()
    {
        Artists = await context.Artists.ToListAsync();
    }
        
    public IActionResult OnGetModalCreate()
    {
        return Partial("CreateModal");
    }

    public IActionResult OnPostModalCreate()
    {
        return Partial("Success", this);
    }
        
    public IActionResult OnGetModalEdit(int id)
    {
        Artist = context.Artists.Find(id) ?? throw new ArgumentNullException("context.Artists.Find(id)");
        return Partial("EditModal", Artist);
    }
        
    public async Task<IActionResult> OnPostModalEdit([FromForm] Artist artist)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Attach(artist).State = EntityState.Modified;

        if (!ArtistExists(artist.Id))
        {
            return NotFound();
        }
        else
        {
            Artist = artist;
            context.Artists.Update(Artist);
            await context.SaveChangesAsync();
        }

        return Partial("_EditSuccess", this);
    }
        
    public IActionResult OnGetModalDelete(int id)
    {
        Artist = context.Artists.Find(id) ?? throw new ArgumentNullException("context.Artists.Find(id)");
        return Partial("DeleteModal", Artist);
    }
        
    public async Task<IActionResult> OnPostModalDelete([FromForm] int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artist = await context.Artists.FindAsync(id);
        if (artist != null)
        {
            Artist = artist;
            context.Artists.Remove(Artist);
            await context.SaveChangesAsync();
        }
        return Partial("_DeleteSuccess", Artist);
    }
        
    public IActionResult OnGetModalDetails(int? id)
    {
        Artist artist = context.Artists.Find(id) ?? throw new ArgumentNullException("context.Artists.Find(id)");
        return Partial("DetailsModal", artist);
    }
    
    private bool ArtistExists(int id)
    {
        return context.Artists.Any(e => e.Id == id);
    }
}