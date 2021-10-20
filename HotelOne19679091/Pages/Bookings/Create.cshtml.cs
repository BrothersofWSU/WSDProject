using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelOne19679091.Data;
using HotelOne19679091.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelOne19679091.Pages.Bookings
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public CreateModel(HotelOne19679091.Data.ApplicationDbContext context)
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
            string email = User.FindFirst(ClaimTypes.Name).Value;

            // Check if room is already booked (exists inside Room)
            Room room = await _context.Room.FirstOrDefaultAsync(r => r.roomId == Booking.roomId);

            if (room != null) // room exists
            {
                Booking.roomId = room.roomId; 
                Booking.customerEmail = email;
                Booking.cost = (decimal)((Booking.checkOut - Booking.checkIn).TotalDays) * room.price;
            }
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
