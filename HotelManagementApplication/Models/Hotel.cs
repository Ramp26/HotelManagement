namespace HotelManagementApplication.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Pic { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string? Ratings { get; set; }
        public int TotalRooms { get; set; }

        public ICollection<Rooms>? rooms { get; set; }
    }
}
