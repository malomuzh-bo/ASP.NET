using System.ComponentModel.DataAnnotations;

namespace ASP_1711.Models
{
    public class QuestRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TransitTime { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int MinYear { get; set; }
        public string Address { get; set; }
        public ICollection<PhoneNum> PhoneNums { get; set; }
        public ICollection<Email> Emails { get; set; }
        public string Company { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        [Range(0, 5)]
        public int ScaryLevel { get; set; }
        [Range(0, 5)]
        public int HardLevel { get; set; }
        public string LogoPath { get; set; }
        public ICollection<PicPath> PicPaths { get; set; }
    }
}
