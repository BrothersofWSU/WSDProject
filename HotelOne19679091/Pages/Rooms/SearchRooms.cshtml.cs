using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelOne19679091.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HotelOne19679091.Pages.Rooms
{
    [Authorize]
    public class SearchRoomsModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public SearchRoomsModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SearchRoomViewModel userInput { get; set; }

        //list for all the rooms
        public IList<Room> Rooms { get; set; }


        public IActionResult OnGet()
        {
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            ViewData["RoomsAvailable"] = new SelectList(_context.Room, "level", "bedCount", "price");

            var bedCount = new SqliteParameter("bedCount", userInput.bedCount);
            var checkIn = new SqliteParameter("checkIn", userInput.checkIn);
            var checkOut = new SqliteParameter("checkOut", userInput.checkOut);

            String sqlStatement = "SELECT * FROM Room " +
                "WHERE bedCount = @bedCount " +
                "AND roomId " +
                "NOT IN (" +
                "SELECT roomId FROM Booking " +
                "WHERE (checkIn BETWEEN @checkIn AND @checkOut) " +
                "AND (checkOut BETWEEN @checkIn AND @checkOut)" +
                ")";


            var roomsQuery = _context.Room.FromSqlRaw(sqlStatement, bedCount, checkIn, checkOut).ToListAsync();

            Rooms = await roomsQuery;

            return Page();
        }
    }
}
