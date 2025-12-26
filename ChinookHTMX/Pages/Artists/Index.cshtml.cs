using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChinookHTMX.Pages.Artists;

public class IndexModel(Data.ChinookContext context) : PageModel
{
    public IList<Artist> Artist { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        var query = context.Artists.Include(a => a.Albums).AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            query = query.Where(a => a.Name.Contains(SearchTerm));
        }

        Artist = await query.OrderBy(a => a.Name).ToListAsync();

        // Return partial view for HTMX requests
        if (Request.Headers.ContainsKey("HX-Request"))
        {
            return Partial("_ArtistsContent", this);
        }

        return Page();
    }
}