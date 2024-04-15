using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Customers;

public class DetailsModel(ChinookHTMX.Data.ChinookContext context) : PageModel
{
    public Customer Customer { get; set; } = default!;

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
}