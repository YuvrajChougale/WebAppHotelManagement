using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppHotelManagement.Models;
using WebAppHotelManagement.ViewModel;

namespace WebAppHotelManagement.Controllers
{
    public class BookingController : Controller
    {
        private HotelDBEntities objHotelDbEntities;
        public BookingController()
        {
            objHotelDbEntities = new HotelDBEntities();
        }
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfRooms = (from objRoom in objHotelDbEntities.Rooms
                                               where objRoom.BookingStatusId == 2
                                               select new SelectListItem()
                                               {
                                                   Text = objRoom.RoomNumber,
                                                   Value = objRoom.RoomId.ToString()
                                               }
                                               ).ToList();
            return View(objBookingViewModel);
        }
        [HttpPost]
        public JsonResult Index(BookingViewModel objBookingViewModel)
        {
            string dt = objBookingViewModel.BookingTo.ToString("mm/dd/yyyy");
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}