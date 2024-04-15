using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.MediaTypes;

public class CreateModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
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

            context.MediaTypes.Add(MediaType);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
}