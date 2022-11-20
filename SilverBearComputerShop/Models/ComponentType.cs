using System.Collections.Generic;

namespace SilverBearComputerShop.Models
{
	public class ComponentType
	{
		public int ID { get; set; }
		public string Type { get; set; }
		public ICollection<Component> Component { get; set; }
	}
}
