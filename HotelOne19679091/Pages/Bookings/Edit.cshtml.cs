﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelOne19679091.Data;
using HotelOne19679091.Models;

namespace HotelOne19679091.Pages.Bookings
{
    public class EditModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public EditModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking = await _context.Booking
                .Include(b => b.theCustomer)
                .Include(b => b.theRoom).FirstOrDefaultAsync(m => m.bookindId == id);

            if (Booking == null)
            {
                return NotFound();
            }
           ViewData["customerEmail"] = new SelectList(_context.Set<Customer>(), "email", "email");
           ViewData["roomId"] = new SelectList(_context.Set<Room>(), "roomId", "level");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(Booking.bookindId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.bookindId == id);
        }
    }
}