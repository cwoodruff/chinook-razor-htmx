using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Employees;

public class DeleteModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Employee Employee { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Employee = await context.Employees
            .Include(e => e.Customers) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Employee == null)
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
            return Partial("_DeleteError", "Invalid employee ID.");
        }

        Employee = await context.Employees
            .Include(e => e.Customers) // Include related data for relationship checks
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Employee == null)
        {
            return Partial("_DeleteError", "Employee not found. It may have been already deleted.");
        }

        try
        {
            // Check for related records
            if (Employee.Customers.Any())
            {
                return Partial("_DeleteError",
                    $"Cannot delete employee '{Employee.LastName}' because it has {Employee.Customers.Count} associated customer(s). Please reassign the customers first.");
            }

            var employeeName = Employee.LastName;
            context.Employees.Remove(Employee);
            await context.SaveChangesAsync();

            return Partial("_DeleteSuccess", $"Employee '{employeeName}' has been successfully deleted.");
        }
        catch (Exception ex)
        {
            return Partial("_DeleteError", $"Error deleting employee: {ex.Message}");
        }
    }
}