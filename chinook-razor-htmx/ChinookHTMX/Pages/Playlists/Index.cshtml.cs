using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookHTMX.Data;
using ChinookHTMX.Entities;

namespace ChinookHTMX.Pages.Playlists
{
    public class IndexModel : PageModel
    {
        private readonly ChinookHTMX.Data.ChinookContext _context;

        public IndexModel(ChinookHTMX.Data.ChinookContext context)
        {
            _context = context;
        }

        public IList<Playlist> Playlist { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Playlist = await _context.Playlists.ToListAsync();
        }
    }
}
