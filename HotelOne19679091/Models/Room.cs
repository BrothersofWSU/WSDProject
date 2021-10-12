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
        [RegularExpression(@"^[1-3G]{1}$", ErrorMessage ="Room level can only be either 1, 2, 3, or G")]
        public string level { get; set; }



        [Required]
        [Display(Name ="Beds")]
        [RegularExpression(@"^[1-3]{1}$")]       
        public int bedCount { get; set; }

        [Display(Name ="Price")]
        [DataType(DataType.Currency)]
        [Range(50, 300, ErrorMessage ="Prices can only range from 50 to 300")]
        public decimal price { get; set; }

        ICollection<Booking> TheBookings { get; set; }

    }
}
