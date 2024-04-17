using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Customers;

public class CreateModel(Data.ChinookContext context) : PageModel
{
    public IActionResult OnGet()
    {
        ViewData["SupportRepId"] = new SelectList(context.Employees, "Id", "Id");
        return Page();
    }

    [BindProperty] public Customer Customer { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Customers.Add(Customer);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}