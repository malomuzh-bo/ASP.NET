namespace SharknessShop.Models
{
	public class Storage
	{
		public int Id { get; set; }
		public ICollection<Product> Products { get; set; }
		public float Price { get; set; }

		public Storage()
		{
			Products = new List<Product>();
		}
	}
}
