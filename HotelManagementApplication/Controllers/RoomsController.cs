using HotelManagementApplication.Interfaces;
using HotelManagementApplication.Models;
using HotelManagementApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuppliersApp.Utilities;

namespace HotelManagementApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;

        public RoomsController(IRoomService roomService,IBookingService bookingService)
        {
            _roomService = roomService;
            _bookingService=bookingService;
        }

        [HttpPost]
        public ActionResult AddRoom(Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newroom = _roomService.AddNewRoom(rooms);
                    return Created("", newroom);
                }
                catch (InvalidUserEntry e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest(ModelState.Keys);
        }

        [HttpGet]
        public ActionResult GetAllRoom()
        {

            try
            {
                var result = _roomService.GetAllRoom();
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
                var result = _roomService.GetById(id);
                return Ok(result);
            }
            catch (DataNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateStatus")]
        public ActionResult Updateroom(Rooms room)
        {
            try
            {
                var result = _roomService.UpdateRoom(room);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteRoom")]
        public ActionResult Removeroom(int id)
        {
            try
            {
                var result = _roomService.RemoveRoom(id);
                _bookingService.deletebyroomId(id);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("{id:int}")]
        public ActionResult GetAllRoomsRespectiveHotel(int id)
        {

            try
            {
                var result = _roomService.GetAvailabelRespectiveHotel(id);
                return Ok(result);
            }
            catch (ListNotfoundException ex)
            {
                return NotFound(ex.Message);
            }


        }


    }
}
