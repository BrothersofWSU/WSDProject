using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelOne19679091.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            return Page();
        }
    }
}
