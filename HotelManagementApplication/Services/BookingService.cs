using HotelManagementApplication.Interfaces;

using HotelManagementApplication.Models;
using HotelManagementApplication.Models.DTOs;

using HotelManagementApplication.Utilities;
using SuppliersApp.Utilities;

namespace HotelManagementApplication.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<int, Booking> _repository;
        private readonly IRoomService _room_repository;
        private readonly IHotelService _hotel_repository;

        public BookingService(IRepository<int,Booking> repository,IRoomService room_repository,IHotelService hotel_repository)
        {
            _repository=repository;
            _room_repository=room_repository;
            _hotel_repository=hotel_repository;
        }
       


        public BookingDto AddNewBooking(BookingDto booking)
        {
            if (booking != null)
            {
                var data = dataTransferToEntity(booking);
                var allBookings = GetAllBooking().ToList();

               
                var bookedRoom = allBookings
                    .FirstOrDefault(x =>
                        x.RoomId == data.RoomId &&
                        ((x.Check_In <= data.Check_In && x.Check_Out > data.Check_In) ||
                         (x.Check_In < data.Check_Out && x.Check_Out >= data.Check_Out)));


                var notAvailableRoom = allBookings
                    .Where(x => x.RoomId == data.RoomId).ToList();
                var maxdate= notAvailableRoom.Max(x => x.Check_Out);
                var mindate = notAvailableRoom.Min(x => x.Check_In);



                if (bookedRoom == null)
                {
                    var res = _repository.Add(data);
               

                    return dataTransferToDto(res);
                }
                else
                {
                    
                    throw new UnAvailableRoomException(maxdate,mindate);
                }
            }
            else
            {
                throw new InvalidUserEntry();
            }
        }

        public void deletebyroomId(int roomid)
        {

            var Allbookings = GetAllBooking();
           var resprooms= Allbookings.Where(book=>book.RoomId == roomid).ToList();
            foreach (var item in resprooms)
            {
                RemoveBooking(item.RoomId);

            }

        }

        public ICollection<BookingDto> GetAllBooking()
        {
            var list = _repository.GetAll();
            List<BookingDto> bookings = new List<BookingDto>();
            if (list != null)
            {

                foreach (var item in list)
                {
                    var res= dataTransferToDto(item);
                    bookings.Add(res);
                }

                return bookings;
            }
            else
            {
                throw new ListNotfoundException();
            }
        }

        public List<RoombookingDto> Getbookingdates(int roomid)
        {

            List<RoombookingDto> roombookingDtos = new List<RoombookingDto>();
            RoombookingDto roombookingDto = new RoombookingDto();
            var Allbookings = GetAllBooking();
            var resprooms = Allbookings.Where(book => book.RoomId == roomid).ToList();
            foreach (var item in resprooms)
            {
                roombookingDto.CheckIn = item.Check_In;
                roombookingDto.Checkout = item.Check_Out;
                roombookingDtos.Add(roombookingDto);

            }

             return  roombookingDtos;
        }

        public BookingDto GetById(int id)
        {
            if (id != null)
            {
                var res= _repository.Get(id);
                return dataTransferToDto(res);
            }
            else
            {
                throw new InvalidUserEntry();
            }
        }

        public Booking RemoveBooking(int id)
        {
            if (id != null && id > 0)
            {
                return _repository.Delete(id);

            }
            else
            {
                throw new InvalidUserEntry();
            }
        }

        public BookingDto UpdateBooking(BookingDto booking)
        {
            if (booking != null)
            {
                var book=dataTransferToEntity(booking);
                var data = _repository.Get(booking.BookingId);
              

                if (data != null)
                {
                    data.CustomerName = book.CustomerName;
                    data.AdharNumber = book.AdharNumber;
                    data.Check_In = book.Check_In;
                    data.Check_Out = book.Check_Out;
                    data.RoomId = book.RoomId;
                    data.HotelId= book.HotelId;
                    data.UserId = book.UserId;
                    data.ContactNumber = book.ContactNumber;
                    var res= _repository.Update(data);
                    return dataTransferToDto(res);
                }
                else
                {
                    throw new InvalidIDException();
                }
            }
            else
            {
                throw new InvalidUserEntry();
            }

        }



        private BookingDto dataTransferToDto(Booking booking)
        {
            var room = _room_repository.GetById(booking.RoomId);
            var hotel = _hotel_repository.GetById(booking.HotelId);

            BookingDto dto = new BookingDto();
            dto.BookingId = booking.BookingId;
            dto.CustomerName = booking.CustomerName;
            dto.AdharNumber = booking.AdharNumber;
            dto.Check_In = booking.Check_In;
            dto.Check_Out = booking.Check_Out;
            dto.ContactNumber = booking.ContactNumber;
            dto.RoomId = booking.RoomId;
            dto.RoomType = room.Type;
            dto.UserId = booking.UserId;
            dto.HotelName = hotel.Name;

            return dto;



        }

        private Booking dataTransferToEntity(BookingDto booking)
        {
          
            var hotel = _hotel_repository.GetByName(booking.HotelName);
            var room = _room_repository.GetByType(booking.RoomType,hotel.Id);

            Booking book = new Booking();
            book.BookingId = booking.BookingId;
            book.CustomerName = booking.CustomerName;
            book.AdharNumber = booking.AdharNumber;
            book.Check_In = booking.Check_In;
            book.Check_Out = booking.Check_Out;
            book.ContactNumber = booking.ContactNumber;
            book.RoomId = room.RoomId;
            book.UserId = booking.UserId;
            book.HotelId = hotel.Id;

            return book;



        }
    }
}
