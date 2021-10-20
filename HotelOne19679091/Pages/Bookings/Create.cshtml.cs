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
using Microsoft.EntityFrameworkCore;

namespace HotelOne19679091.Pages.Bookings
{
    [Authorize(Roles = "Customers")]
    public class CreateModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public CreateModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Room room { get; set; }

        public IActionResult OnGet()
        {
        ViewData["customerEmail"] = new SelectList(_context.Customer, "email", "email");
        ViewData["roomId"] = new SelectList(_context.Room, "roomId", "level");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Room rooms = await _context.Room.FirstOrDefaultAsync();
            room = await _context.Room.FirstOrDefaultAsync();

            Booking.customerEmail = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Booking.cost = (decimal)((Booking.checkOut - Booking.checkIn).TotalDays) * rooms.price;

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
