using Microsoft.EntityFrameworkCore;
using Slots.Models.Domain;

namespace Slots.Data
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

            Database.EnsureCreated();
        }

        public DbSet<Drink> Drinks { get; set; }





    }
}
