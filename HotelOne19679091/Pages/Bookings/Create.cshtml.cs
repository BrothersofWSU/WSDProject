using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelOne19679091.Data;
using HotelOne19679091.Models;

namespace HotelOne19679091.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public CreateModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["customerEmail"] = new SelectList(_context.Set<Customer>(), "email", "email");
        ViewData["roomId"] = new SelectList(_context.Set<Room>(), "roomId", "level");
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

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
