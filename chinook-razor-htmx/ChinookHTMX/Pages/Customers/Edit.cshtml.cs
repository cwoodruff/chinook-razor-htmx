using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChinookHTMX.Pages.Customers;

public class EditModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Customer Customer { get; set; } = default!;

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Customer = await context.Customers.FirstOrDefaultAsync(m => m.Id == id);
        
        ViewData["SupportRepId"] = new SelectList(context.Employees, "Id", "Id");

        if (Customer == null)
        {
            return NotFound();
        }

        if (Request.IsHtmx())
        {
            return Partial("EditModal", this);
        }

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Return validation errors as partial view
            return Partial("_ValidationErrors", ModelState);
        }

        context.Attach(Customer).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"Customer '{Customer.LastName}' updated successfully!");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(Customer.Id))
            {
                return Partial("_ErrorMessage", "Customer not found. It may have been deleted by another user.");
            }
            else
            {
                return Partial("_ErrorMessage", "A concurrency error occurred. Please refresh and try again.");
            }
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error updating customer: {ex.Message}");
        }
    }

    // Keep the modal method for backward compatibility
    public async Task<IActionResult> OnPostModalAsync()
    {
        return await OnPostAsync();
    }

    private bool CustomerExists(int id)
    {
        return context.Customers.Any(e => e.Id == id);
    }
}