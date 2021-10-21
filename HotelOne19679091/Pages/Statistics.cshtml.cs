using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelOne19679091.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HotelOne19679091.Pages
{
    [Authorize(Roles="Admin")]
    public class StatisticsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public StatisticsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PostcodeStats> PostcodeStat { get; set; }
        public IList<RoomStats> RoomStat { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var postcodeGroup = _context.Customer.GroupBy(c => c.postCode);
            var roomstatsGroup = _context.Booking.GroupBy(b => b.roomId);

            PostcodeStat = await postcodeGroup.Select(p => new PostcodeStats { Postcode = p.Key, Customers = p.Count() }).ToListAsync();
            RoomStat = await roomstatsGroup.Select(p => new RoomStats { RoomID = p.Key, Bookings = p.Count() }).ToListAsync();

            return Page();
        }
    }
}
