using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;

namespace ChinookHTMX.Pages.Invoices;

public class EditModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public Invoice Invoice { get; set; } = default!;

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Invoice = await context.Invoices.FirstOrDefaultAsync(m => m.Id == id);
        ViewData["CustomerId"] = new SelectList(context.Customers, "Id", "LastName");

        if (Invoice == null)
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

        context.Attach(Invoice).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"Invoice '{Invoice.Id}' updated successfully!");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InvoiceExists(Invoice.Id))
            {
                return Partial("_ErrorMessage", "Invoice not found. It may have been deleted by another user.");
            }
            else
            {
                return Partial("_ErrorMessage", "A concurrency error occurred. Please refresh and try again.");
            }
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error updating invoice: {ex.Message}");
        }
    }

    // Keep the modal method for backward compatibility
    public async Task<IActionResult> OnPostModalAsync()
    {
        return await OnPostAsync();
    }

    private bool InvoiceExists(int id)
    {
        return context.Invoices.Any(e => e.Id == id);
    }
}