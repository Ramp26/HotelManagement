namespace HotelManagementApplication.Models.DTOs
{
    public class HotelDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public string Rating { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int AvailableAcRooms { get; set; }
        public int AvailableNonAcRooms { get; set; }
        public int TotalRoooms { get; set; }

        public float AcRoomPrice { get; set; }
        public float NonAcRoomPrice { get; set; }
      
           
       
}
}
