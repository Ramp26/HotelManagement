using FirstAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementApplication.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string AdharNumber { get; set; }
        public DateTime Check_In { get; set; }
        public DateTime Check_Out { get; set; }
        public string ContactNumber { get; set; }

        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Rooms? room { get; set; }

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel? hotel { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? user { get; set; }

      
    }
}
