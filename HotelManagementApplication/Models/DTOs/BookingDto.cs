namespace HotelManagementApplication.Models.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string AdharNumber { get; set; }
        public DateTime Check_In { get; set; }
        public DateTime Check_Out { get; set; }
        public string ContactNumber { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public string RoomType { get; set; }
        public string HotelName { get; set; }
    }
}
