using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SilverBearComputerShop.Models
{
	public class Component
	{
		public int ID { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name = "Component Name")]
		public string Name { get; set; }

		[Display(Name = "Component Type")]
		public int ComponentTypeId { get; set; }

		[Display(Name = "Component Type")]
		public ComponentType ComponentType { get; set; }

		[Display(Name = "Computer Component")]
		public ICollection<ComputerComponent> ComputerComponent { get; set; }

	}
}
