using Microsoft.EntityFrameworkCore;
using Parky.API.Models;

namespace Parky.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trial> Trials { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
