using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Genres;

public class DetailsModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    public Genre Genre { get; set; } = default!;

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
}