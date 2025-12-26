using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Albums;

public class IndexModel(Data.ChinookContext context) : PageModel
{
    public IList<Album> Album { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        var query = context.Albums.Include(a => a.Artist).AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            query = query.Where(a => a.Title.Contains(SearchTerm) ||
                                   (a.Artist != null && a.Artist.Name.Contains(SearchTerm)));
        }

        Album = await query.OrderBy(a => a.Title).ToListAsync();

        // Return partial view for HTMX requests
        if (Request.Headers.ContainsKey("HX-Request"))
        {
            return Partial("_AlbumsContent", this);
        }

        return Page();
    }
}