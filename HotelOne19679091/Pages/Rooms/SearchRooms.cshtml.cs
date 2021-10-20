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
            context = _context;
        }

        [BindProperty]
        public SearchRoomViewModel userInput { get; set; }

        //list for all the rooms

        public IList<Room> DiffRooms { get; set; }

        public IActionResult OnGet()
        {
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           

            //constructing the query to fine all rooms with the number of beds required.

            var appRooms = _context.Room.FromSqlInterpolated($"SELECT * FROM Room where bedCount = {userInput.bedCount} and roomId NOT IN (SELECT * FROM Room WHERE (checkIn between {userInput.checkIn} AND {userInput.checkOut}) AND (checkOut between {userInput.checkIn} AND {userInput.checkOut}) )");

            DiffRooms = await appRooms.ToListAsync();

            ViewData["RoomsAvailable"] = new SelectList(_context.Room, "level","bedCount", "price");


            return Page();
        }
    }
}
