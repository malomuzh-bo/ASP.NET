using ASP_1711.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASP_1711.Pages
{
	public class AdminPanelModel : PageModel
	{
        RoomsContext db;
        public List<QuestRoom> QuestRooms { get; set; }
        public AdminPanelModel(RoomsContext db)
        {
            this.db = db;
        }

        public async Task OnGetAsync()
        {
            QuestRooms = await db.QuestRooms.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            db.QuestRooms.Remove(await db.QuestRooms.FindAsync(id));
            db.PhoneNums.RemoveRange(await db.PhoneNums.Where(q => q.QuestRoomId == id).ToListAsync());
            db.PicPaths.RemoveRange(await db.PicPaths.Where(q => q.QuestRoomId == id).ToListAsync());
            db.Emails.RemoveRange(await db.Emails.Where(q => q.QuestRoomId == id).ToListAsync());

            await db.SaveChangesAsync();

            return RedirectToPage();
        }

    }
}
