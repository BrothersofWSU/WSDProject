using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelOne19679091.Data;
using HotelOne19679091.Models;

namespace HotelOne19679091.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public IndexModel(HotelOne19679091.Data.ApplicationDbContext context)
        {

            Console.WriteLine("i am in the index page");
            _context = context;
        }

        public IList<Booking> Booking { get;set; }

        public async Task OnGetAsync()
        {
            Booking = await _context.Booking
                .Include(b => b.theCustomer)
                .Include(b => b.theRoom).ToListAsync();
        }
    }
}
