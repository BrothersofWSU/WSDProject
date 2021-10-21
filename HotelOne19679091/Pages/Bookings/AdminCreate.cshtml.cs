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
using Microsoft.Data.Sqlite;
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
            ViewData["customerEmail"] = new SelectList(_context.Customer, "email", "FullName");
            ViewData["roomId"] = new SelectList(_context.Room, "roomId", "roomId");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["customerEmail"] = new SelectList(_context.Customer, "email", "FullName");
            ViewData["roomId"] = new SelectList(_context.Room, "roomId", "roomId");

            if (Booking.checkIn > Booking.checkOut)
            {
                ViewData["oninvaliddate"] = "true";
                return Page();
            }

            var roomId = new SqliteParameter("roomId", Booking.roomId);
            var checkIn = new SqliteParameter("checkIn", Booking.checkIn);
            var checkOut = new SqliteParameter("checkOut", Booking.checkOut);

            String sqlStatement = "SELECT * FROM Room as r " +
                "WHERE r.roomId = @roomId " +
                "AND roomId IN (" +
                "SELECT roomId FROM Booking " +
                "WHERE (checkIn BETWEEN @checkIn AND @checkOut) " +
                "AND (checkOut BETWEEN @checkIn AND @checkOut)" +
                ")";


            var query = _context.Room.FromSqlRaw(sqlStatement, roomId, checkIn, checkOut);

            // Check if room is already booked (exists inside Room)
            var rooms = await query.ToListAsync();


            if (rooms.Count() > 0) // room exists
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
