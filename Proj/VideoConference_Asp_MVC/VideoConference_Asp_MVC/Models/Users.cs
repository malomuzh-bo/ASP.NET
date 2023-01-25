using Microsoft.AspNetCore.Identity;

namespace VideoConference_Asp_MVC.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public Roles role { get; set; }
        public override string ToString()
        {
            return $"{Lastname}";
        }
    }
}
