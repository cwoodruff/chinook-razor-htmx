using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Customers;

public class DeleteModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    [BindProperty] public Customer Customer { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await context.Customers.FirstOrDefaultAsync(m => m.Id == id);

        if (customer == null)
        {
            return NotFound();
        }
        else
        {
            Customer = customer;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await context.Customers.FindAsync(id);
        if (customer != null)
        {
            Customer = customer;
            context.Customers.Remove(Customer);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}