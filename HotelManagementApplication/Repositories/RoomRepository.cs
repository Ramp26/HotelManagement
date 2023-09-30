using HotelManagementApplication.Contexts;
using HotelManagementApplication.Interfaces;
using HotelManagementApplication.Models;
using HotelManagementApplication.Utilities;

namespace HotelManagementApplication.Repositories
{
    public class RoomRepository :IRepository<int,Rooms>
    {
        private readonly HotelContext _hotelContext;

        public RoomRepository( HotelContext hotelContext)
        {
            _hotelContext=hotelContext;
        }

        public Rooms Add(Rooms item)
        {
            _hotelContext.rooms.Add(item);
            _hotelContext.SaveChanges();
            return item;
        }

        public Rooms Delete(int key)
        {
            var product = Get(key);
            if (product != null)
            {
                _hotelContext.rooms.Remove(product);
                _hotelContext.SaveChanges();
                return product;
            }
            return null;
        }

        public Rooms Get(int key)
        {
            
            var rooms = _hotelContext.rooms.FirstOrDefault(room => room.RoomId == key);
            if(rooms != null)
            {
                return rooms;
            }
            else
            {
                throw new InvalidIDException();
            }
         
        }

        public List<Rooms> GetAll()
        {
            return _hotelContext.rooms.ToList();
        }

        public Rooms Update(Rooms item)
        {
            _hotelContext.Entry<Rooms>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _hotelContext.SaveChanges();
            return item;
        }
    }
}
