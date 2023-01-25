namespace ASP_0511_HW
{
	public class Room
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Info { get; set; }
		public string? Image { get; set; }
		public override string ToString()
		{
			return "Id: " + Id + "\nName: " + Name + "\nInfo: " + Info;
		}
	}
}
