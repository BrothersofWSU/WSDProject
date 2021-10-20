using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelOne19679091.Data;
using HotelOne19679091.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelOne19679091.Pages.Rooms
{
    [Authorize(Roles = "Customers")]
    public class IndexModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public IndexModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; }

        public async Task OnGetAsync()
        {
            Room = await _context.Room.ToListAsync();
        }
    }
}
