using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.MediaTypes;

public class DeleteModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    [BindProperty] public MediaType MediaType { get; set; } = default!;

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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
            if (id == null)
            {
                return NotFound();
            }

            var mediatype = await context.MediaTypes.FindAsync(id);
            if (mediatype != null)
            {
                MediaType = mediatype;
                context.MediaTypes.Remove(MediaType);
                await context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
}