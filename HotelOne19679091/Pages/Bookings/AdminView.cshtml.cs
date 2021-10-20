using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelOne19679091.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelOne19679091.Pages.Bookings
{
    public class AdminViewModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public AdminViewModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get; set; }

        public async Task OnGetAsync()
        {
            Booking = await _context.Booking
                .Include(b => b.theCustomer)
                .Include(b => b.theRoom).ToListAsync();
        }
    }
}
