

using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Models;

namespace RoyalVilla_API.Database
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {


        public DbSet<Villa> Villa { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "This is the Royal Villa",
                    Rate = 200.0,
                    Occupancy = 4,
                    Sqft = 550,
                    ImageUrl = "",
                    CreatedDate = new DateTime(2026,1,1),
                    
                },
                new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Details = "This is the Premium Pool Villa",
                    Rate = 300.0,
                    Occupancy = 4,
                    Sqft = 550,
                    ImageUrl = "",
                    CreatedDate = new DateTime(2026, 1, 1)
                }
            );
        }
    }
}
