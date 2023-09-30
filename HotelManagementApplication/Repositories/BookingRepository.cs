using HotelManagementApplication.Contexts;
using HotelManagementApplication.Interfaces;
using HotelManagementApplication.Models;
using HotelManagementApplication.Utilities;

namespace HotelManagementApplication.Repositories
{
    public class BookingRepository : IRepository<int, Booking>
    {
        private readonly HotelContext _hotelContext;

        public BookingRepository(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public Booking Add(Booking item)
        {

            _hotelContext.bookings.Add(item);
            _hotelContext.SaveChanges();
            return item;
        }

        public Booking Delete(int key)
        {
            var booking = Get(key);
            if (booking != null)
            {
                _hotelContext.bookings.Remove(booking);
                _hotelContext.SaveChanges();
                return booking;
            }
            return null;
        }

        public Booking Get(int key)
        {
            var hotels = _hotelContext.bookings.FirstOrDefault(room => room.BookingId == key);
            if (hotels != null)
            {
                return hotels;
            }
            else
            {
                throw new InvalidIDException();
            }
        }

        public List<Booking> GetAll()
        {
            return _hotelContext.bookings.ToList();
        }

        public Booking Update(Booking item)
        {
            _hotelContext.Entry<Booking>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _hotelContext.SaveChanges();
            return item;
        }
    }
}
