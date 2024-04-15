using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Genres;

public class EditModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    [BindProperty] public Genre Genre { get; set; } = default!;

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

            Genre = genre;
            return Page();
        }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            context.Attach(Genre).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(Genre.Id))
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

    private bool GenreExists(int id)
    {
            return context.Genres.Any(e => e.Id == id);
        }
}