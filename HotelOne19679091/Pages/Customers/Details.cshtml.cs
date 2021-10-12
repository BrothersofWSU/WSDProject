using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelOne19679091.Data;
using HotelOne19679091.Models;

namespace HotelOne19679091.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;

        public DetailsModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customer.FirstOrDefaultAsync(m => m.email == id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
