using HotelManagementApplication.Models;
using HotelManagementApplication.Models.DTOs;

namespace HotelManagementApplication.Interfaces
{
    public interface IRoomService
    {
        public Rooms AddNewRoom(Rooms rooms);
        public Rooms GetById(int id);

        public Rooms GetByType(string type,int hotelId);
        public Rooms RemoveRoom(int id);
        public ICollection<Rooms> GetAllRoom();
        public Rooms UpdateRoom(Rooms rooms);
        public List<Rooms> SearchByPrice(float Minimum_price, float Maximum_Price);
        public HotelDTO GetAvailabaleroom(int hotelId);

        public RoomsDto GetAvailabelRespectiveHotel(int hotelId);

        public Boolean Updatestatus(int roomId);

    }
}
