using HotelManagementApplication.Models;
using HotelManagementApplication.Models.DTOs;

namespace HotelManagementApplication.Interfaces
{
    public interface IBookingService
    {

        public BookingDto AddNewBooking(BookingDto booking);
        public BookingDto GetById(int id);
        public Booking RemoveBooking(int id);
        public void deletebyroomId(int roomid);
        public  List<RoombookingDto> Getbookingdates(int roomid);

        

        public ICollection<BookingDto> GetAllBooking();
        public BookingDto UpdateBooking(BookingDto booking);

    }
}
