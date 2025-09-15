using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Employees;

public class CreateModel(Data.ChinookContext context) : PageModel
{
    public IActionResult OnGet()
    {
        ViewData["ReportsTo"] = new SelectList(context.Employees, "Id", "Id");
        return Page();
    }

    [BindProperty] public Employee Employee { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Partial("_ValidationErrors", ModelState);
        }
        
        try
        {
            context.Employees.Add(Employee);
            await context.SaveChangesAsync();

            // Return success message
            return Partial("_SuccessMessage", $"Employee '{Employee.LastName}' created successfully!");
        }
        catch (Exception ex)
        {
            // Return error message
            return Partial("_ErrorMessage", $"Error creating employee: {ex.Message}");
        }
    }
}