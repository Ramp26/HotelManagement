using HotelManagementApplication.Interfaces;
using HotelManagementApplication.Models;
using HotelManagementApplication.Models.DTOs;
using HotelManagementApplication.Services;
using HotelManagementApplication.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuppliersApp.Utilities;

namespace HotelManagementApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService1;

        public BookingController(IBookingService bookingService,IRoomService roomService,IBookingService bookingService1)
        {
            _bookingService=bookingService;
            _roomService = roomService;
            _bookingService1=bookingService1;
        }

        [HttpPost]
        public ActionResult AddBooking(BookingDto book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    
                        var newbooking = _bookingService.AddNewBooking(book);
                        return Created("", newbooking);
                   

                  
                }
                catch (InvalidUserEntry e)
                {
                    return BadRequest(e.Message);
                }catch (UnAvailableRoomException e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest(ModelState.Keys);
        }


        [HttpGet]
        public ActionResult GetAllBooks()
        {

            try
            {
                var result = _bookingService.GetAllBooking().ToList();
                return Ok(result);
            }
            catch (ListNotfoundException ex)
            {
                return NotFound(ex.Message);
            }


        }


        [HttpGet("GetAllBookedDatesfromRoomId")]
        public ActionResult GetAllBookedDatesfromRoomId(int roomId)
        {

            try
            {
                var result = _bookingService.Getbookingdates(roomId).ToList();
                return Ok(result);
            }
            catch (ListNotfoundException ex)
            {
                return NotFound(ex.Message);
            }


        }


        [HttpGet("GetById")]
        public ActionResult GetById(int id)
        {

            try
            {
                var result = _bookingService.GetById(id);
                return Ok(result);
            }
            catch (DataNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateBooking")]
        public ActionResult UpdateBooking(BookingDto book)
        {
            try
            {
                var result = _bookingService.UpdateBooking(book);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteBooking")]
        public ActionResult Removehotel(int id)
        {
            try
            {
                var result = _bookingService.RemoveBooking(id);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
