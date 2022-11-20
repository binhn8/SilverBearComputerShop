using System;
using System.Collections.Generic;

namespace SilverBearComputerShop.Models
{
	public class Computer
	{
		public int ID { get; set; }
		public string Weight { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public ICollection<ComputerComponent> ComputerComponent { get; set; }
	}
}
