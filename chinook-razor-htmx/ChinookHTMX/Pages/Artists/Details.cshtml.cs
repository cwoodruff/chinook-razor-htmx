using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Artists;

public class DetailsModel(Data.ChinookContext context) : PageModel
{
    public Artist Artist { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Artist = await context.Artists.FirstOrDefaultAsync(m => m.Id == id);

        if (Artist == null)
        {
            return NotFound();
        }

        if (HttpContext.Request.Headers["HX-Request"].FirstOrDefault() == "true")
        {
            return Partial("Artists/DetailsModal", this);
        }

        return Page();
    }
}