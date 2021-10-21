using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelOne19679091.Models
{
    public class Booking
    {
        [Key]
        public int bookingId { get; set; }

        [Display(Name = "Room ID")]
        public int roomId { get; set; } //this has to be a foreign key to Rooms table

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string customerEmail { get; set; }

        [Display(Name ="Check in date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}")]
        public DateTime checkIn { get; set; }

        [Display(Name = "Check out date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime checkOut { get; set; }

        [Display(Name ="Total cost of booking")]
        [DataType(DataType.Currency)]
        public decimal cost { get; set; }

        [Display(Name ="The Room:")]
        public Room theRoom { get; set; }

        [Display(Name = "Customer")]
        public Customer theCustomer { get; set; }


    }
}
