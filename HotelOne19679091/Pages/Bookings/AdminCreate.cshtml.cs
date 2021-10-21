using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotelOne19679091.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelOne19679091.Pages.Bookings
{
    [Authorize]
    public class AdminCreateModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public AdminCreateModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["customerEmail"] = new SelectList(_context.Customer, "email", "email");
            ViewData["roomId"] = new SelectList(_context.Room, "roomId", "roomId");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["roomId"] = new SelectList(_context.Room, "roomId", "roomId");

            if (Booking.checkIn > Booking.checkOut)
            {
                ViewData["oninvaliddate"] = "true";
                return Page();
            }

            FormattableString sqlStatement = $"SELECT * FROM Room as r, Booking as b WHERE b.roomId == r.roomId AND b.roomId == {Booking.roomId} and ({Booking.checkIn} >= b.checkIn and {Booking.checkIn} <= b.checkOut) or ({Booking.checkOut} >= b.checkIn and {Booking.checkOut} <= b.checkOut)";
            var booking = _context.Room.FromSqlInterpolated(sqlStatement);

            string email = User.FindFirst(ClaimTypes.Name).Value;

            // Check if room is already booked (exists inside Room)
            Room rooms = await _context.Room.FirstOrDefaultAsync(r => r.roomId == Booking.roomId);

            if (booking.Count() == 0) // room exists
                Booking.cost = (decimal)((Booking.checkOut - Booking.checkIn).TotalDays) * rooms.price;

            else
            {
                ViewData["AlreadyBooked"] = "true";
                return Page();
            }

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();
            ViewData["Success"] = "true";

            return Page();
        }
    }
}
