﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelOne19679091.Models
{
    public class Customer
    {
        public int cutomerId { get; set; }

        [Display(Name ="Customer email")]
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string email { get; set; }

        [Display(Name ="Surname")]
        [Required]
        [MinLength(2), MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z'-]*$", ErrorMessage = "Surname can only be between 2 and 20 chacaters and can only consist of English letters, hyphen and apostrophe.")]
        public string lanme { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [MinLength(2), MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z'-]*$", ErrorMessage = "First name can only be between 2 and 20 chacaters and can only consist of English letters, hyphen and apostrophe.")]
        public string fname { get; set; }

        [Display(Name ="Post code")]
        [Required]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage ="Post needs to be exactly 4 digits long")]
        public string postCode { get; set; }

        public ICollection<Booking> theBookings { get; set; }
    }
}