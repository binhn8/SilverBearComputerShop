namespace SilverBearComputerShop.Models
{
	public class ComputerComponent
	{
        public int ID { get; set; }
        public int ComputerID { get; set; }
        public int ComponentID { get; set; }
        public Computer Computer { get; set; }
        public Component Component { get; set; }
    }
}
