namespace ASP_1711.Models
{
    public class PicPath
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int QuestRoomId { get; set; }
        public virtual QuestRoom? QuestRoom { get; set; }
    }
}
