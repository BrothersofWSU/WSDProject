using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelOne19679091.Data;
using HotelOne19679091.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HotelOne19679091.Pages.Bookings
{
    [Authorize]

namespace HotelOne19679091.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public IndexModel(HotelOne19679091.Data.ApplicationDbContext context)
        {

            Console.WriteLine("i am in the index page");
            _context = context;
        }

        public IList<Booking> Booking { get;set; }

        [ViewData]
        public string nextDateSort { get; set; }
        [ViewData]
        public string nextCostSort { get; set; }



        public async Task OnGetAsync(string sortOrder)
        {
            if (String.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "date_asc";
            }

            IQueryable<Booking> bookings = _context.Booking;


            switch (sortOrder)
            {
                case "date_asc":
                    bookings = bookings.OrderBy(b => b.checkIn);
                    break;
                case "date_desc":
                    bookings = bookings.OrderByDescending(b => b.checkIn);
                    break;

                case "cost_asc":
                    bookings = bookings.OrderBy(b => (double)b.cost);
                    break;
                case "cost_desc":
                    bookings = bookings.OrderByDescending(b => (double)b.cost);
                    break;
            }

            nextDateSort = sortOrder != "date_asc" ? "date_asc" : "date_desc";
            nextCostSort = sortOrder != "cost_asc" ? "cost_asc" : "cost_desc";

            string email = User.FindFirst(ClaimTypes.Name).Value;
            
            Booking = await bookings
                .Where(b => b.customerEmail == email)
                .AsNoTracking()
                .Include(b => b.theCustomer)
                .Include(b => b.theRoom).ToListAsync();
        }
    }
}
