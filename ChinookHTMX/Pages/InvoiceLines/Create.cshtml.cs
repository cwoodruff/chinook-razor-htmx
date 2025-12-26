using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.InvoiceLines;

public class CreateModel(Data.ChinookContext context) : PageModel
{
    public IActionResult OnGet()
    {
        ViewData["InvoiceId"] = new SelectList(context.Invoices, "Id", "Id");
        ViewData["TrackId"] = new SelectList(context.Tracks, "Id", "Id");
        return Page();
    }

    [BindProperty] public InvoiceLine InvoiceLine { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Partial("_ValidationErrors", ModelState);
        }
        
        try
        {
            context.InvoiceLines.Add(InvoiceLine);
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"Invoice Line '{InvoiceLine.Id}' created successfully!");
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error creating invoice line: {ex.Message}");
        }
    }
}