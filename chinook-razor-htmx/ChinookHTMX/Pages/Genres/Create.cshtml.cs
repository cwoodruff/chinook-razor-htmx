using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Genres
{
    public class CreateModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public CreateModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] public Genre Genre { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Genres.Add(Genre);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}