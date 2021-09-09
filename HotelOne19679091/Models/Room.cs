using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelOne19679091.Models
{
    public class Room
    {
        public int roomId { get; set; }

        [Required]
        [Display(Name ="Level")]
        [RegularExpression(@"^[1,2,3]{1}$", ErrorMessage ="")]
        public string level { get; set; }



        [Required]
        [Display(Name ="Beds")]
        
        public int bedCount { get; set; }

    }
}
