using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoConference_Asp_MVC.Models;

namespace VideoConference_Asp_MVC.Controllers
{
    public class MainController : Controller
    {
        ConferenceDBContext db = null!;
        public Users User = null!;
        [BindProperty]
        public Rooms room { get; set; }
        [BindProperty]
        public Users user { get; set; }
        [BindProperty]
        public Roles role { get; set; }
        public MainController(ConferenceDBContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult MainPage(int id)
        {
            User = db.Users.Where(o => o.Id == id).FirstOrDefault();
            return View(User);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add(string Name, int userId,string description)
        {
            Rooms room = new Rooms() { Name = Name, Description = description };
            UserToRoom utr = new UserToRoom();
            user = db.Users.Where(o => o.Id == userId).AsNoTracking().FirstOrDefault();
            await db.Rooms.AddAsync(room);
            await db.SaveChangesAsync();
            room = db.Rooms.Where(o => o.Name == room.Name).AsNoTracking().FirstOrDefault();
            utr.RoomId = room.Id;
            utr.UserId = user.Id;
            await db.userToRooms.AddAsync(utr);
            return RedirectToAction("MainPage", "Main");
        }
        public IActionResult ShowRoom()
        {
            return View(db.Rooms);
        }
        public IActionResult AddRoom()
        {
            return View();
        }
    }
}
