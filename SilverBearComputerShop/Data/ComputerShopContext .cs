using Microsoft.EntityFrameworkCore;
using SilverBearComputerShop.Models;

namespace SilverBearComputerShop.Data
{
    public class ComputerShopContext : DbContext
    {
		public ComputerShopContext(DbContextOptions<ComputerShopContext> options) : base(options)
        {
        }

        public DbSet<Computer> Computer { get; set; }
        public DbSet<ComputerComponent> ComputerComponent { get; set; }
        public DbSet<Component> Component { get; set; }
        public DbSet<ComponentType> ComponentType { get; set; }
    }
}
