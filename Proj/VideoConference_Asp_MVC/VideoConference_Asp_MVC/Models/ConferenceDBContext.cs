using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace VideoConference_Asp_MVC.Models
{
    public class ConferenceDBContext:DbContext
    {
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Roles> Roles { get; set; } = null!;
        public DbSet<Rooms> Rooms { get; set; } = null!;
        public DbSet<UserToRoom> userToRooms { get; set; } = null!;
        public ConferenceDBContext(DbContextOptions<ConferenceDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
