using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelOne19679091.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HotelOne19679091.Pages.Rooms
{
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

        public IList<Booking> DiffRooms { get; set; }

        public IActionResult OnGet()
        {
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var NumbRooms = new SqliteParameter("NumbRooms",userInput.bedCount);
            var checkIn = new SqliteParameter("checkIn", userInput.checkIn);
            var checkOut = new SqliteParameter("checkOut", userInput.checkOut);

            var appRooms = _context.Booking.FromSqlRaw("select * from [Room] where bedCount = @NumbRooms", NumbRooms);
            string query = "SELECT * from Room where bedCount = @NumbRooms";
           // Room room = await _context.Room.FromSqlRaw(query, NumbRooms);
            //constructing the query to fine all rooms with the number of beds required.
            Console.WriteLine("This is the user bedcount " + userInput.bedCount + " checkin: " + userInput.checkIn + " checkout: " + userInput.checkOut);

            /*var appRooms = _context.Booking.FromSqlInterpolated($"SELECT * FROM Room as r, Booking as b WHERE r.bedCount == {userInput.bedCount}");*/
            Console.WriteLine(appRooms);
            DiffRooms = await appRooms.ToListAsync();

            ViewData["RoomsAvailable"] = new SelectList(_context.Room, "level","bedCount", "price"); 


            return Page();
        }
    }
}
