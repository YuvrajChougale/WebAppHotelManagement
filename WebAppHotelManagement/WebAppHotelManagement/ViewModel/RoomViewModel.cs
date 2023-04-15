using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.WebPages.Html;
using System.ComponentModel.DataAnnotations;

namespace WebAppHotelManagement.ViewModel
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }

        [Display(Name ="Room No.")]
        [Required (ErrorMessage ="Room Number is required.")]
        public string RoomNumber { get; set; }

        [Display(Name = " Room Image")]
        [Required(ErrorMessage = "Room Image is required.")]
        public string RoomImages { get; set; }

        [Display(Name = "Room Price")]
        [Required(ErrorMessage = "Room Price is required.")]
        [Range(500, 10000, ErrorMessage = "Room price should be equal and greater than {1}")]
        public decimal RoomPrice { get; set; }

        [Display(Name = "Booking Status")]
        [Required(ErrorMessage = "Booking status is required.")]
        public int BookingStatusId { get; set; }

        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "Room Type is required.")]
        [Range(1,8,ErrorMessage ="Room capacity should be equal and greater than {1}")]
        public int RoomTypeId { get; set; }

        [Display(Name = "Room Capacity")]
        [Required(ErrorMessage = "Room Capacity is required.")]
        public int RoomCapacity { get; set; }

        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Room Discription")]
        [Required(ErrorMessage = "Room Description is required.")]
        public string RoomDescription { get; set; }

    
       // public List<SelectListItem> ListOfBookingStatus { get; set; }
        public List<SelectListItem> ListOfBookingStatus { get; set; }

        public List<SelectListItem> ListOfRoomType { get; set; }

    }
}