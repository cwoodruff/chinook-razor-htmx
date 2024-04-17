using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.MediaTypes;

public class DetailsModel(Data.ChinookContext context) : PageModel
{
    public MediaType MediaType { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var mediatype = await context.MediaTypes.FirstOrDefaultAsync(m => m.Id == id);
        if (mediatype == null)
        {
            return NotFound();
        }
        else
        {
            MediaType = mediatype;
        }

        return Page();
    }
}