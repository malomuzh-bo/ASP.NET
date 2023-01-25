namespace ASP_1711.Models
{
    public class PhoneNum
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int QuestRoomId { get; set; }
        public virtual QuestRoom? QuestRoom { get; set; }
    }
}
