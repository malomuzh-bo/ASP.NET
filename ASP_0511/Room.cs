namespace ASP_0511
{
	public class Room
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Info { get; set; }
		public override string ToString()
		{
			return "Id: " + Id + "\nName: " + Name + "\nInfo: " + Info;
		}
	}
}
