using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.MediaTypes
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

        [BindProperty] public MediaType MediaType { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MediaTypes.Add(MediaType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}