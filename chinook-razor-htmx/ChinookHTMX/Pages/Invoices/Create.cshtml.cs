using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Invoices;

public class CreateModel(Data.ChinookContext context) : PageModel
{
    public IActionResult OnGet()
    {
        ViewData["CustomerId"] = new SelectList(context.Customers, "Id", "Id");
        return Page();
    }

    [BindProperty] public Invoice Invoice { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Partial("_ValidationErrors", ModelState);
        }
        
        try
        {
            context.Invoices.Add(Invoice);
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"Invoice '{Invoice.Id}' created successfully!");
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error creating invoice: {ex.Message}");
        }
    }
}