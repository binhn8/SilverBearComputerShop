using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SilverBearComputerShop.Models
{
	public class ComponentType
	{
		public int ID { get; set; }
		[Required]
		[StringLength(100)]
		[Display(Name = "Component Type")]
		public string Type { get; set; }
		public ICollection<Component> Component { get; set; }
	}
}
