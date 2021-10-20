using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HotelOne19679091.Models;

namespace HotelOne19679091.Pages.Customers
{
    [Authorize]
    public class MyDetailsModel : PageModel
    {
        private readonly HotelOne19679091.Data.ApplicationDbContext _context;



        public MyDetailsModel(HotelOne19679091.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CustomerViewModel Myself { get; set; } //where form data will be bound to.

        public async Task<IActionResult> OnGetAsync()
        {
            //retrieve the logged in users email
            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Customer customer = await _context.Customer.FirstOrDefaultAsync(m => m.email == _email);

            if(customer != null)
            {
                //the customer is already in the database
                ViewData["ExistInDB"] = "true";
                Myself = new CustomerViewModel
                {
                    fname = customer.fname,
                    lname = customer.lname,
                    postCode = customer.postCode
                };
                
            }
            else
            {
                ViewData["ExistInDB"] = "false";
            }

            return Page();
        }

        /*public void OnGet()
        {
        }*/

        public async Task<IActionResult> OnPostAsync()
        {
            //retrieve the logged in users email
            string _email = User.FindFirst(ClaimTypes.Name).Value;
            
            Customer customer = await _context.Customer.FirstOrDefaultAsync(m => m.email == _email);

            if(customer != null)
            {
                //this user is already in the database
                ViewData["ExistInDB"] = "true";
            }
            else
            {
                ViewData["ExistInDB"] = "false";
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (customer == null)
            {
                //create a new customer to be added into the database
                customer = new Customer();
            }

            //construct the customer object based on 'MySelf'
            customer.email = _email;
            customer.fname = Myself.fname;
            customer.lname = Myself.lname;
            customer.postCode = Myself.postCode;
            if ((string)ViewData["ExistInDB"] == "true")
            {
                _context.Attach(customer).State = EntityState.Modified;
            }
            else
            {
                _context.Customer.Add(customer);
            }

            try //catching the conflict of editing this record currently
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            ViewData["SuccessDB"] = "success";
            return Page();
        }




    }
}
