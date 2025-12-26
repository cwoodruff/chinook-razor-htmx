using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Tracks;

public class DetailsModel(Data.ChinookContext context) : PageModel
{
    public Track Track { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Track = await context.Tracks
            .Include(t => t.Album)
            .Include(t => t.MediaType)
            .Include(t => t.Genre)
            .Include(t => t.InvoiceLines)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Track == null)
        {
            return NotFound();
        }

        if (Request.IsHtmx())
        {
            return Partial("DetailsModal", this);
        }

        return Page();
    }
}