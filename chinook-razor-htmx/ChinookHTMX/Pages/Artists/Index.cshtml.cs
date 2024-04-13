using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Artists
{
    public class IndexModel(ChinookHTMX.Data.ChinookContext context) : PageModel
    {
        public IList<Artist> Artist { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Artist = await context.Artists.ToListAsync();
        }
        
        public IActionResult OnGetModalCreate()
        {
            return Partial("CreateModal");
        }

        public IActionResult OnPostModalCreate()
        {
            return Partial("Success", this);
        }
        
        public IActionResult OnGetModalEdit()
        {
            return Partial("EditModal");
        }
        
        public IActionResult OnGetModalDelete()
        {
            return Partial("DeleteModal");
        }
        
        public IActionResult OnGetModalDetails()
        {
            return Partial("DetailsModal");
        }
    }
}