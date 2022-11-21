using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SilverBearComputerShop.Models
{
	public class Computer
	{
		public int ID { get; set; }

		[Required]
		[StringLength(10)]
		[Display(Name = "Weight")]
		public string Weight { get; set; }

		[Required]
		[StringLength(200)]
		[Display(Name = "Title")]
		public string Title { get; set; }
		public string Description { get; set; }

		[Display(Name = "Computer Component")]
		public ICollection<ComputerComponent> ComputerComponent { get; set; }
	}
}
