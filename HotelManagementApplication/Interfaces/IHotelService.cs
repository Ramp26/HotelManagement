using HotelManagementApplication.Models;

namespace HotelManagementApplication.Interfaces
{
    public interface IHotelService
    {
        public Hotel AddNewHotel(Hotel hotel);
        public Hotel GetById(int id);
        public Hotel GetByName(string name);
        public Hotel RemoveHotel(int id);
        public ICollection<Hotel> GetAllHotel();
        public Hotel UpdateHotel(Hotel hotel);
        public List<Hotel> SearchByLocation(string location);
        public List<Hotel> getAllfilterdhotels(List<int?> ids);

    }
}
