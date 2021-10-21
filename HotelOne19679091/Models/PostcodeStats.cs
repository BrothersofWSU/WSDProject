using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelOne19679091.Models
{
    public class PostcodeStats
    {
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Display(Name ="Customer")]
        public int Customers { get; set; }
    }
}
