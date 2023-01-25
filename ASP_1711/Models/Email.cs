namespace ASP_1711.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string EmailName { get; set; }
        public int QuestRoomId { get; set; }
        public virtual QuestRoom? QuestRoom { get; set; }
    }
}
