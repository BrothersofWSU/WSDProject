using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelOne19679091.Models
{
    public class RoomStats
    {
        [Display(Name = "RoomID")]
        public int RoomID { get; set; }

        [Display(Name = "Number of Bookings")]
        public int Bookings { get; set; }
    }
}
