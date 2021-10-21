using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelOne19679091.Models
{
    public class SearchRoomViewModel
    {
        [Required]
        [Display(Name = "Beds")]
        [Range(1, 3)]
        public int bedCount { get; set; }

        [Display(Name = "Check in date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:dd-MM-yyyy")]
        public DateTime checkIn { get; set; }

        [Display(Name = "Check out date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:dd-MM-yyyy")]
        public DateTime checkOut { get; set; }
    }
}
