using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;
using Htmx;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChinookHTMX.Pages.InvoiceLines;

public class EditModel(Data.ChinookContext context) : PageModel
{
    [BindProperty] public InvoiceLine InvoiceLine { get; set; } = default!;

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        InvoiceLine = await context.InvoiceLines.FirstOrDefaultAsync(m => m.Id == id);
        ViewData["InvoiceId"] = new SelectList(context.Invoices, "Id", "Id");

        if (InvoiceLine == null)
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

        context.Attach(InvoiceLine).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"InvoiceLine '{InvoiceLine.Id}' updated successfully!");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InvoiceLineExists(InvoiceLine.Id))
            {
                return Partial("_ErrorMessage", "Invoice Line not found. It may have been deleted by another user.");
            }
            else
            {
                return Partial("_ErrorMessage", "A concurrency error occurred. Please refresh and try again.");
            }
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error updating invoice line: {ex.Message}");
        }
    }

    // Keep the modal method for backward compatibility
    public async Task<IActionResult> OnPostModalAsync()
    {
        return await OnPostAsync();
    }

    private bool InvoiceLineExists(int id)
    {
        return context.InvoiceLines.Any(e => e.Id == id);
    }
}