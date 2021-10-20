using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HotelOne19679091.Models;

namespace HotelOne19679091.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HotelOne19679091.Models.Booking> Booking { get; set; }
        public DbSet<HotelOne19679091.Models.Customer> Customer { get; set; }
        public DbSet<HotelOne19679091.Models.Room> Room { get; set; }
    }
}
