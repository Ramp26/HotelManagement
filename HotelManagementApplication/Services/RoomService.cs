using HotelManagementApplication.Interfaces;
using HotelManagementApplication.Models;
using HotelManagementApplication.Models.DTOs;
using HotelManagementApplication.Utilities;
using SuppliersApp.Utilities;

namespace HotelManagementApplication.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<int, Rooms> _repository;

        public RoomService(IRepository<int, Rooms> repository)
        {
            _repository = repository;
        }
        public Rooms AddNewRoom(Rooms rooms)
        {
            if (rooms != null)
            {
                rooms.Isbooked = false;
                return _repository.Add(rooms);
            }
            else
            {
                throw new InvalidUserEntry();
            }
        }

        public ICollection<Rooms> GetAllRoom()
        {
            var list = _repository.GetAll();
            if (list != null)
            {
                return list;
            }
            else
            {
                throw new ListNotfoundException();
            }
        }

        public HotelDTO GetAvailabaleroom(int hotelId)
        {
            HotelDTO hotel = new HotelDTO();
            var rooms=GetAllRoom();
            var AvailabaleACRoom=rooms.Count(room=>room.HotelId==hotelId &&room.Type=="Ac" && room.Isbooked==false);
            var AvailabaleNonAcRoom = rooms.Count(room => room.HotelId == hotelId && room.Type == "Non-Ac" && room.Isbooked == false);
            var AcRoomPrice = rooms.Where(room => room.HotelId == hotelId && room.Type == "Ac").Select(room=>room.Price).FirstOrDefault();
            var NonAcRoomPrice = rooms.Where(room => room.HotelId == hotelId && room.Type == "Non-Ac").Select(room => room.Price).FirstOrDefault();

            hotel.AvailableAcRooms = AvailabaleACRoom;
            hotel.AvailableNonAcRooms = AvailabaleNonAcRoom;
            hotel.AcRoomPrice = AcRoomPrice;
            hotel.NonAcRoomPrice= NonAcRoomPrice;

            return hotel;
        }

        public RoomsDto GetAvailabelRespectiveHotel(int hotelId)
        {
            RoomsDto roomsDto=new RoomsDto();
            var rooms = GetAllRoom();
            var respectiveAvailbelRooms=rooms.Where(roo=>roo.HotelId==hotelId && roo.Isbooked==false).ToList();
            var respectiveBookedRooms = rooms.Where(roo => roo.HotelId == hotelId && roo.Isbooked == true).ToList();
            roomsDto.AvailableRooms = respectiveAvailbelRooms;
            roomsDto.BookedRooms= respectiveBookedRooms;
            return roomsDto;
        }

        public Rooms GetById(int id)
        {
            if (id != null)
            {
                return _repository.Get(id);
            }
            else
            {
                throw new InvalidUserEntry();
            }
        }

        public Rooms GetByType(string type, int hotelId)
        {

            var rooms = GetAllRoom();
            var room = rooms.Where(roo=> roo.Type==type && roo.Isbooked==false &&roo.HotelId==hotelId).FirstOrDefault();
            return room;

        }

        public Rooms RemoveRoom(int id)
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

        public List<Rooms> SearchByPrice(float Minimum_price, float Maximum_Price)
        {
          var allRooms= GetAllRoom();
           
           var  selectedRoom = allRooms.Where(room=>room.Price>Minimum_price && room.Price<Maximum_Price).ToList();
            if (selectedRoom!=null)
            {
                return selectedRoom;
            }
            else
            {
                return null;
            }
           
        }

        public Rooms UpdateRoom(Rooms rooms)
        {
            if(rooms != null)
            {
                var data=_repository.Get(rooms.RoomId);
                if(data != null)
                {
                    data.Type = rooms.Type;
                    data.Price = rooms.Price;
                    data.Isbooked=rooms.Isbooked;               
                    return _repository.Update(data);
                }

                throw new InvalidIDException();
            }
            throw new InvalidUserEntry();
        }

        public bool Updatestatus(int roomId)
        {
            var data = GetById(roomId);
          if(data.Isbooked == false)
            {
                data.Isbooked = true;
                _repository.Update(data);
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
