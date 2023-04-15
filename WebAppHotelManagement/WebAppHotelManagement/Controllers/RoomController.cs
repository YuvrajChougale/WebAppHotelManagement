using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppHotelManagement.Models;
using WebAppHotelManagement.ViewModel;


namespace WebAppHotelManagement.Controllers
{
    public class RoomController : Controller
    {
        HotelDBEntities objHotelDbEntites;
        public RoomController()
        {
            objHotelDbEntites = new HotelDBEntities();
        }
        // GET: Room
        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objHotelDbEntites.BookingStatus
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.BookingStatus,
                                                        Value = obj.BookingStatusId.ToString()
                                                    }).ToList();

            objRoomViewModel.ListOfRoomType = (from obj in objHotelDbEntites.RoomTypes
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoomTypeName,
                                                   Value = obj.RoomTypeId.ToString()
                                               }).ToList();

            return View(objRoomViewModel);
        }
        [HttpPost]
        public ActionResult Index(RoomViewModel objRoomViewModel)
        {
            string message = string.Empty;
            string ImageUniqueName = string.Empty;
            string ActualImageName = string.Empty;

            if (objRoomViewModel.RoomId == 0)
            {
                 ImageUniqueName = Guid.NewGuid().ToString();
                 ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                 objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));

                Room objRoom = new Room()
                {
                    RoomNumber = objRoomViewModel.RoomNumber,
                    RoomImages = ActualImageName,
                    RoomPrice = objRoomViewModel.RoomPrice,
                    IsActive = true,
                    BookingStatusId = objRoomViewModel.BookingStatusId,
                    RoomTypeId = objRoomViewModel.RoomTypeId,
                    RoomCapacity = objRoomViewModel.RoomCapacity,
                    RoomDescription = objRoomViewModel.RoomDescription,
                };
                objHotelDbEntites.Rooms.Add(objRoom);
                message = "Added.";
            }
            else
            {
                Room objRoom = objHotelDbEntites.Rooms.Single(model => model.RoomId == objRoomViewModel.RoomId);

                if (objRoomViewModel.Image != null)
                {
                    ImageUniqueName = Guid.NewGuid().ToString();
                    ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                    objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));
                    objRoom.RoomImages = ActualImageName;
                }
                objRoom.RoomNumber = objRoomViewModel.RoomNumber;
                objRoom.RoomPrice = objRoomViewModel.RoomPrice;
                objRoom.IsActive = true;
                objRoom.BookingStatusId = objRoomViewModel.BookingStatusId;
                objRoom.RoomTypeId = objRoomViewModel.RoomTypeId;
                objRoom.RoomCapacity = objRoomViewModel.RoomCapacity;
                objRoom.RoomDescription = objRoomViewModel.RoomDescription;
                message = "Updated.";
            }
            objHotelDbEntites.SaveChanges();
            return Json(data: new{ message = "Room successfully "+ message , success = true}, behavior:JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listofRoomDetailsViewModels =
                (from objRoom in objHotelDbEntites.Rooms
                 join objBooking in objHotelDbEntites.BookingStatus on objRoom.BookingStatusId equals objBooking.BookingStatusId
                 join objRoomType in objHotelDbEntites.RoomTypes on objRoom.RoomTypeId equals objRoomType.RoomTypeId
                 where objRoom.IsActive == true
                 select new RoomDetailsViewModel()

                 {
                     RoomNumber = objRoom.RoomNumber,
                     RoomDescription = objRoom.RoomDescription,
                     RoomCapacity = objRoom.RoomCapacity,
                     RoomPrice = objRoom.RoomPrice,
                     BookingStatus = objBooking.BookingStatus,
                     RoomType = objRoomType.RoomTypeName,
                     RoomImage = objRoom.RoomImages,
                     RoomId = objRoom.RoomId
                 }).ToList();
            return PartialView("_RoomDetailsPartial", listofRoomDetailsViewModels);
        }
        [HttpGet]
        public JsonResult EditRoomDetails(int roomId)
        {
            var result = objHotelDbEntites.Rooms.Single(model => model.RoomId == roomId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRoomDetails(int roomId)
        {
            Room objRoom = objHotelDbEntites.Rooms.Single(model => model.RoomId == roomId);
            objRoom.IsActive = false;
            objHotelDbEntites.SaveChanges();
            return Json(new {message ="Record successfuly Deleted.", success= true },JsonRequestBehavior.AllowGet);
        }
       
    }   
}