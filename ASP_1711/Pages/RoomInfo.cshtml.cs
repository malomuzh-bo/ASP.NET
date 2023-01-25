using ASP_1711.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP_1711.Pages
{
    public class RoomInfoModel : PageModel
    {
        RoomsContext db;
        public QuestRoom? QuestRoom { get; set; } = new();
        public List<PhoneNum>? PhoneNums { get; set; } = new();
        public List<Email>? Emails { get; set; } = new();
        public List<PicPath>? PicPaths { get; set; } = new();

        public RoomInfoModel(RoomsContext db)
        {
            this.db = db;
        }
        public async Task OnGetAsync(int id)
        {
            QuestRoom = await db.QuestRooms.FindAsync(id);
            PhoneNums = await db.PhoneNums.Where(q => q.QuestRoomId == id).ToListAsync();
            Emails = await db.Emails.Where(q => q.QuestRoomId == id).ToListAsync();
            PicPaths = await db.PicPaths.Where(q => q.QuestRoomId == id).ToListAsync();
        }
    }
}
