using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChinookHTMX.Pages.Customers;

public class IndexModel(Data.ChinookContext context) : PageModel
{
    public IList<Customer> Customer { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        var query = context.Customers.Include(a => a.Invoices).AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            query = query.Where(a => a.LastName.Contains(SearchTerm));
        }

        Customer = await query.OrderBy(a => a.LastName).ToListAsync();

        // Return partial view for HTMX requests
        if (Request.Headers.ContainsKey("HX-Request"))
        {
            return Partial("_CustomersContent", this);
        }

        return Page();
    }
}