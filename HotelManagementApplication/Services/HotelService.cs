using HotelManagementApplication.Contexts;
using HotelManagementApplication.Interfaces;
using HotelManagementApplication.Models;
using HotelManagementApplication.Utilities;
using Microsoft.AspNetCore.Authorization;
using SuppliersApp.Utilities;

namespace HotelManagementApplication.Services
{
    public class HotelService : IHotelService
    {
        private readonly IRepository<int, Hotel> _repository;

        public HotelService(IRepository<int,Hotel> repository)
        {
            _repository = repository;
        }

        public Hotel AddNewHotel(Hotel hotel)
        {
           if(hotel != null)
            {
              return  _repository.Add(hotel);
            }
            else
            {
                throw new InvalidUserEntry();
            }


        }

        public List<Hotel> getAllfilterdhotels(List<int?> ids)
        {
            if (ids != null)
            {
                 List<Hotel> hotels=new List<Hotel>();
                foreach (var item in ids)
                {
                   var data= _repository.Get(item??0);
                    hotels.Add(data);
                }
                return hotels;
              
            }
            else
            {
                throw new InvalidUserEntry();
            }

        }

        public ICollection<Hotel> GetAllHotel()
        {
           var list= _repository.GetAll();
            if(list != null)
            {
                return list;
            }
            else
            {
                throw new ListNotfoundException();
            }
        }

        public Hotel GetById(int id)
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

        public Hotel GetByName(string name)
        {
          var hotels=  GetAllHotel().ToList();
            var hotel= hotels.Where(hot=> hot.Name == name).FirstOrDefault();
            return hotel;
        }

        public Hotel RemoveHotel(int id)
        {
            if(id !=null && id > 0)
            {
              return _repository.Delete(id);

            }
            else
            {
                throw new InvalidUserEntry();
            }
        }

        public List<Hotel> SearchByLocation(string location)
        {
            var hotels = GetAllHotel();

            var datas =hotels.Where(hot=>hot.Location == location).ToList();
            return datas;
        }

        public Hotel UpdateHotel(Hotel hotel)
        {
            if (hotel != null)
            {
                var data=GetById(hotel.Id);
                if(data != null)
                {
                    data.Name=hotel.Name;
                    data.Location = hotel.Location;
                    data.TotalRooms = hotel.TotalRooms;
                    data.Address = hotel.Address;
                    data.Ratings = hotel.Ratings;
                    data.PhoneNumber = hotel.PhoneNumber;
                    data.Pic = hotel.Pic;
                    return _repository.Update(data);
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
    }
}
