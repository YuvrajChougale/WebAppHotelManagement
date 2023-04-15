using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppHotelManagement.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name ="Customer Name")]
        [Required(ErrorMessage ="Customer name is required")]
        public string CustomerName { get; set; }

        [Display(Name ="Customer Address")]
        [Required(ErrorMessage = "Customer Address is required")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number is required")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Booking Form")]
        [Required(ErrorMessage = "Booking from date is required")]
        [DataType(DataType.Date)]
        public DateTime BookingFrom { get; set; }

        [Display(Name = "Booking To")]
        [Required(ErrorMessage = "Booking TO date is required")]
        [DataType(DataType.Date)]
        public DateTime BookingTo { get; set; }

        [Display(Name = "Assign Room")]
        [Required(ErrorMessage = "Assign Room is required")]
        public int AssignRoomId { get; set; }

        [Display(Name = "Number of Member")]
        [Required(ErrorMessage = "number of  member is required")]
        public int NumberOfMembers { get; set; }

        public IEnumerable<SelectListItem> ListOfRooms { get; set; }
       
    }
}