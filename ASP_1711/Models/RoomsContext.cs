using Microsoft.EntityFrameworkCore;

namespace ASP_1711.Models
{
    public class RoomsContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }
        public DbSet<PicPath> PicPaths { get; set; }
        public DbSet<PhoneNum> PhoneNums { get; set; }
        public DbSet<QuestRoom> QuestRooms { get; set; }
        public RoomsContext(DbContextOptions opt) : base(opt)
        {
            Database.EnsureCreated();
        }
    }
}
