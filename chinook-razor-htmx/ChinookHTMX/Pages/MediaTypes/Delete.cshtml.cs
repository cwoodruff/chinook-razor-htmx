using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Data;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.MediaTypes
{
    public class DeleteModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public DeleteModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        [BindProperty] public MediaType MediaType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediatype = await _context.MediaTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (mediatype == null)
            {
                return NotFound();
            }
            else
            {
                MediaType = mediatype;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediatype = await _context.MediaTypes.FindAsync(id);
            if (mediatype != null)
            {
                MediaType = mediatype;
                _context.MediaTypes.Remove(MediaType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}