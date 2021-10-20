using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelOne19679091.Data;
using HotelOne19679091.Models;

namespace HotelOne19679091.Pages.Rooms
{
    public class DetailsModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public DetailsModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Room Room { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Room.FirstOrDefaultAsync(m => m.roomId == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
