using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Customers;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Customer Customer { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Customer = await context.Customers
            .Include(c => c.Invoices) // Include related data for relationship checks
            .Include(c => c.SupportRepId == Customer.SupportRepId) // Add other related entities if needed
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Customer == null)
        {
            return NotFound();
        }

        // Return modal for HTMX requests
        if (Request.IsHtmx())
        {
            return Partial("DeleteModal", this);
        }

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return Partial("_DeleteError", "Invalid customer ID.");
        }

        Customer = await context.Customers
            .Include(c => c.Invoices) // Include related data for relationship checks
            .Include(c => c.SupportRepId == Customer.SupportRepId) // Add other related entities if needed
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Customer == null)
        {
            return Partial("_DeleteError", "Customer not found. It may have been already deleted.");
        }

        try
        {
            // Check for related records
            if (Customer.Invoices.Any())
            {
                return Partial("_DeleteError",
                    $"Cannot delete customer '{Customer.LastName}' because it has {Customer.Invoices.Count} associated invoice(s). Please delete the invoices first.");
            }

            var customerLastName = Customer.LastName;
            context.Customers.Remove(Customer);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"Customer '{customerLastName}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting customer: {ex.Message}");
        }
    }
}