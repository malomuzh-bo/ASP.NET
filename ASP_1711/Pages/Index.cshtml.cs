using ASP_1711.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP_1711.Pages
{
	public class IndexModel : PageModel
	{
		RoomsContext db;
		public List<QuestRoom> Rooms { get; set; } = new();
		public List<QuestRoom> FiltRooms { get; set; } = new();
		public IndexModel(RoomsContext db)
		{
			this.db = db;
			FiltRooms = Rooms = db.QuestRooms.AsNoTracking().ToList();
		}

		public void OnGet()
		{
		}
		public async Task<IActionResult> OnPostFiltAsync(int minRate, int maxRate, int minScare, int maxScare, int minPlayers, int maxPlayers)
		{
			if (minRate != 0 && minScare != 0 && minPlayers != 0)
			{
				FiltRooms = await db.QuestRooms.Where(q => q.Rating >= minRate && q.Rating <= maxRate
				&& q.ScaryLevel >= minScare && q.ScaryLevel <= maxScare && q.MinPlayers >= minPlayers
				&& q.MaxPlayers <= maxPlayers).AsNoTracking().ToListAsync();
			}
			else if (minRate != 0 && minPlayers != 0)
			{
				FiltRooms = await db.QuestRooms.Where(q => q.Rating >= minRate && q.Rating <= maxRate
				&& q.MinPlayers >= minPlayers && q.MaxPlayers <= maxPlayers).AsNoTracking().ToListAsync();
			}
			else if (minPlayers != 0 && minScare != 0)
			{
				FiltRooms = await db.QuestRooms.Where(q => q.MinPlayers >= minPlayers && q.MaxPlayers <= maxPlayers
				&& q.ScaryLevel >= minScare && q.ScaryLevel <= maxScare).AsNoTracking().ToListAsync();
			}
			else if (minScare != 0 && minRate != 0)
			{
				FiltRooms = await db.QuestRooms.Where(q => q.ScaryLevel >= minScare && q.ScaryLevel <= maxScare
				&& q.Rating >= minRate && q.Rating <= maxRate).AsNoTracking().ToListAsync();
			}
			else if (minRate != 0)
				FiltRooms = await db.QuestRooms.Where(q => q.Rating >= minRate && q.Rating <= maxRate).AsNoTracking().ToListAsync();
			else if (minPlayers != 0)
				FiltRooms = await db.QuestRooms.Where(q => q.MinPlayers >= minPlayers && q.MaxPlayers <= maxPlayers).AsNoTracking().ToListAsync();
			else if (minScare != 0)
				FiltRooms = await db.QuestRooms.Where(q => q.ScaryLevel >= minScare && q.ScaryLevel <= maxScare).AsNoTracking().ToListAsync();
			else
				FiltRooms = Rooms;
			return Page();
		}
		public async Task<IActionResult> OnPostClearFiltAsync(int id)
		{
			FiltRooms = Rooms;
			return Page();
		}
	}
}