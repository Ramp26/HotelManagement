using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementApplication.Models
{
    public class Rooms
    {
        [Key]
        public int RoomId { get; set; }
        public string Pic { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public bool Isbooked { get; set; }

        public int? HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel? hotel { get; set; }

    }
}
