using SilverBearComputerShop.Data;
using SilverBearComputerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SilverBearComputerShopTest
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(ComputerShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Computer.Any())
            {
                return;   // DB has been seeded
            }

            var computer = new Computer[]
            {
                new Computer{Weight="8.1 kg", Title="ASUS E210MA 11.6", Description="This is a wonderful machine"},
                new Computer{Weight="12 kg" , Title="ASUS E210MA 11.6", Description="This is a wonderful machine"},
                new Computer{Weight="16 lb", Title="ASUS E210MA 11.6", Description="This is a wonderful machine"}
            };
            foreach (Computer item in computer)
            {
                context.Computer.Add(item);
            }
            context.SaveChanges();

            var componentType = new ComponentType[]
            {
                new ComponentType{Type="RAM"},
                new ComponentType{Type="Storage"}
            };
            foreach (ComponentType item in componentType)
            {
                context.ComponentType.Add(item);
            }
            context.SaveChanges();
        }
    }
}
