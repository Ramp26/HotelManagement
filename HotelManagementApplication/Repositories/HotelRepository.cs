using HotelManagementApplication.Contexts;
using HotelManagementApplication.Interfaces;
using HotelManagementApplication.Models;
using HotelManagementApplication.Utilities;

namespace HotelManagementApplication.Repositories
{
    public class HotelRepository :IRepository<int,Hotel>
    {
        private readonly HotelContext _hotelContext;

        public HotelRepository(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }

        public Hotel Add(Hotel item)
        {
            _hotelContext.hotels.Add(item);
            _hotelContext.SaveChanges();
            return item;
        }

        public Hotel Delete(int key)
        {
            var product = Get(key);
            if (product != null)
            {
                _hotelContext.hotels.Remove(product);
                _hotelContext.SaveChanges();
                return product;
            }
            return null;
        }

        public Hotel Get(int key)
        {
            var hotels = _hotelContext.hotels.FirstOrDefault(room => room.Id == key);
            if(hotels != null)
            {
                return hotels;
            }
            else
            {
                throw new InvalidIDException();
            }
           
        }

        public List<Hotel> GetAll()
        {
            return _hotelContext.hotels.ToList();
        }

        public Hotel Update(Hotel item)
        {
            _hotelContext.Entry<Hotel>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _hotelContext.SaveChanges();
            return item;
        }
    }
}
