using SilverBearComputerShop.Data;
using SilverBearComputerShop.Models;
using System;
using System.Linq;

namespace SilverBearComputerShop.Data
{
	public static class DbInitializer
	{
		public static void Initialize(ComputerShopContext context)
		{
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
                new ComponentType{Type="Storage"},
                new ComponentType{Type="Port  Hub"},
                new ComponentType{Type="Graphic Card"},
                new ComponentType{Type="PSU"},
                new ComponentType{Type="CPU"}
            };
            foreach (ComponentType item in componentType)
            {
                context.ComponentType.Add(item);
            }
            context.SaveChanges();

            var component = new Component[]
            {
                new Component{Name="512 MB",ComponentTypeId=1},
                new Component{Name="2 GB",ComponentTypeId=1},
                new Component{Name="8 GB",ComponentTypeId=1},
                new Component{Name="16 GB",ComponentTypeId=1},
                new Component{Name="32 GB",ComponentTypeId=1},
                new Component{Name="8 GB",ComponentTypeId=1},

                new Component{Name="1 TB SSD",ComponentTypeId=2},
                new Component{Name="2 TB HDD",ComponentTypeId=2},
                new Component{Name="3 TB HDD",ComponentTypeId=2},
                new Component{Name="4 TB HDD",ComponentTypeId=2},
                new Component{Name="80 GB SSD",ComponentTypeId=2},
                new Component{Name="500 GB SDD",ComponentTypeId=2},

            };
            foreach (Component item in component)
            {
                context.Component.Add(item);
            }
            context.SaveChanges();

            var computerComponent = new ComputerComponent[]
            {
                new ComputerComponent{ComputerID=1,ComponentID=1},
                new ComputerComponent{ComputerID=1,ComponentID=2},
                new ComputerComponent{ComputerID=2,ComponentID=1},
                new ComputerComponent{ComputerID=2,ComponentID=1}

            };
            foreach (ComputerComponent item in computerComponent)
            {
                context.ComputerComponent.Add(item);
            }
            context.SaveChanges();


        }
	}
}
