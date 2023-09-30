using HotelManagementApplication.Interfaces;
using HotelManagementApplication.Models;
using HotelManagementApplication.Models.DTOs;
using HotelManagementApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuppliersApp.Utilities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HotelManagementApplication.Controllers
{
    [EnableCors("MyCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;

        public HotelController(IHotelService hotelService,IRoomService roomService)
        {
            _hotelService = hotelService;
            _roomService= roomService;
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddHotel(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newhotel = _hotelService.AddNewHotel(hotel);
                    return Created("", newhotel);
                }
                catch (InvalidUserEntry e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest(ModelState.Keys);
        }


        [HttpGet]
        public ActionResult GetAllHotels()
        {

            try
            {
                var result = _hotelService.GetAllHotel().ToList();
                return Ok(result);
            }
            catch (ListNotfoundException ex)
            {
                return NotFound(ex.Message);
            }


        }

        [Authorize(Roles = "admin,staff")]
        [HttpGet("HotelDetails")]
        public ActionResult HotelDetails(int hotelid)
        {

            try
            {
              
                var result = _roomService.GetAvailabaleroom(hotelid);
                var hotel = _hotelService.GetById(hotelid);
                result.id = hotel.Id;
                result.Name=hotel.Name;
                result.Rating = hotel.Ratings;
                result.Address= hotel.Address;
                result.PhoneNumber = hotel.PhoneNumber;
                result.Location= hotel.Location;
                result.TotalRoooms = hotel.TotalRooms;
                result.Pic=hotel.Pic;
               

                return Ok(result);
            }
            catch (ListNotfoundException ex)
            {
                return NotFound(ex.Message);
            }


        }

        [HttpGet("GetByPrice")]
        public ActionResult SearchHotelByPrice(float min,float max)
        {

            try
            {
            
              var   datalist =_roomService.SearchByPrice(min,max).ToList();
                List<int?> datas = null;
                if (datalist != null)
                {
                    datas = datalist.Select(x => x.HotelId).ToList();

                    var result = _hotelService.getAllfilterdhotels(datas).ToList();

                    return Ok(result);

                }
                else
                {
                    throw new ListNotfoundException();
                }


            }
            catch (ListNotfoundException ex)
            {
                return NotFound(ex.Message);
            }


        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetById")]
        public ActionResult GetById(int id)
        {

            try
            {
                var result = _hotelService.GetById(id);
                return Ok(result);
            }
            catch (DataNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("SearchBylocation")]
        public ActionResult SearchBylocation(string location)
        {

            try
            {
                var result = _hotelService.SearchByLocation(location);
                return Ok(result);
            }
            catch (DataNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPut("UpdateHotel")]
        public ActionResult UpdateHotel(Hotel hotel)
        {
            try
            {
                var result = _hotelService.UpdateHotel(hotel);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteHotel")]
        public ActionResult Removehotel(int id)
        {
            try
            {
                var result = _hotelService.RemoveHotel(id);
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
