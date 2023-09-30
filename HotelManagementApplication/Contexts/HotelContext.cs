using FirstAPI.Models;
using HotelManagementApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementApplication.Contexts
{
    public class HotelContext :DbContext
    {
        public HotelContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Hotel> hotels { get; set; }

        public DbSet<Rooms> rooms { get; set; }
        public DbSet<Booking> bookings { get; set; }

        public DbSet<User> users { get; set; }
    }
}
