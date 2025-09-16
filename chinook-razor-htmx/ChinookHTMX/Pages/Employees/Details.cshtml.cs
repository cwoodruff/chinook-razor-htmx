using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Employees;

public class DetailsModel(Data.ChinookContext context) : PageModel
{
    public Employee Employee { get; set; } = default!;
    
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Employee = await context.Employees
            .Include(e => e.Customers)
            .Include(e => e.ReportsToNavigation)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Employee == null)
        {
            return NotFound();
        }

        if (Request.IsHtmx())
        {
            return Partial("DetailsModal", this);
        }

        return Page();
    }
}