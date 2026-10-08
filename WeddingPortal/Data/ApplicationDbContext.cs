//using Microsoft.EntityFrameworkCore;
//using WeddingPortal.Models;

//namespace WeddingPortal.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(
//            DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Guest> Guests { get; set; }

//        public DbSet<Event> Events { get; set; }

//        public DbSet<WeddingInfo> WeddingInfos { get; set; }
//    }
//}
using Microsoft.EntityFrameworkCore;
using WeddingPortal.Models;

namespace WeddingPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<WeddingInfo> WeddingInfos { get; set; }
    }
}