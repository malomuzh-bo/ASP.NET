namespace VideoConference_Asp_MVC.Models
{
    public class Rooms
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Users> Users { get; set; }
        public string? Description { get; set; }
    }
}
