using System.Collections.Generic;

namespace SilverBearComputerShop.Models
{
	public class Component
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int ComponentTypeId { get; set; }
		public ComponentType ComponentType { get; set; }
		public ICollection<ComputerComponent> ComputerComponent { get; set; }

	}
}
