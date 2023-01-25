using Microsoft.AspNetCore.Mvc;
using VideoConference_Asp_MVC.Models;

namespace VideoConference_Asp_MVC.Controllers
{
    public class MainStarterController : Controller
    {
        public List<Roles> roles;
        public ConferenceDBContext db;
        [BindProperty(Name = "email", SupportsGet = true)]
        public string email { get; set; }
        [BindProperty(Name = "password", SupportsGet = true)]
        public string password { get; set; }
        public MainStarterController(ConferenceDBContext db)
        {
            this.db = db;
            if(db.Roles.ToList().Count == 0)
            {
                roles = new List<Roles>() { new Roles{ RoleName = "admin"}, new Roles { RoleName="user"}};
                foreach(var item in roles)
                {
                    db.Roles.Add(item);
                }
                db.SaveChangesAsync();
            }
        }
        //public async Task<IActionResult> OnPostLoginAsync(int id)
        //{
        //    var user = await db.Users.FindAsync(id);

        //    return RedirectToPage("MainPage");
        //}
        public IActionResult Start()
        {
            return View();
        }
    }
}
