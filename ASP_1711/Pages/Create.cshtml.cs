using ASP_1711.Models;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP_1711.Pages
{
	public class CreateModel : PageModel
	{
		RoomsContext db;
		[BindProperty]
		public QuestRoom? QuestRoom { get; set; }
		[BindProperty]
		public PhoneNum? Number { get; set; }
		[BindProperty]
        public Email? RoomEmail { get; set; }
		[BindProperty]
        public PicPath? Picture { get; set; }

        List<PhoneNum>? PhoneNums { get; set; }
		List<Email>? Emails { get; set; }
		List<PicPath>? PicPaths { get; set; }

		public CreateModel(RoomsContext db)
		{
			this.db = db; 

			QuestRoom = new QuestRoom();
			Number = new PhoneNum();
			RoomEmail = new Email();
			Picture = new PicPath();

			PhoneNums = new List<PhoneNum>();
			Emails = new List<Email>();
			PicPaths = new List<PicPath>();
		}
		public void OnGet()
		{
		}
        public async Task<IActionResult> OnPostAsync()
        {
            var files = Request.Form.Files;
            var dir = $@"{Directory.GetCurrentDirectory()}\wwwroot\Photos\{QuestRoom!.Name.Replace(" ", "_")}";
            Directory.CreateDirectory(dir);
            if (files.Count != 0)
            {
                QuestRoom.LogoPath = $@"{dir}\{files[0].FileName}";
                using (FileStream fs = new FileStream(QuestRoom.LogoPath, FileMode.Create))
                {
                    await files[0].CopyToAsync(fs);
                }
                QuestRoom.LogoPath = $@"{dir}\{files[0].FileName}".Split("wwwroot")[1];
            }

            await db.QuestRooms.AddAsync(QuestRoom!);
            await db.SaveChangesAsync();

            Number!.QuestRoomId = QuestRoom.Id;
            RoomEmail!.QuestRoomId = QuestRoom.Id;

            PhoneNums!.Add(Number);
            Emails!.Add(RoomEmail);
            for (int i = 0; i < 100; i++)
            {
                if (Request.Form[$"phoneNumber{i}"].ToString() != "")
                {
                    Number = new PhoneNum();
                    Number.QuestRoomId = QuestRoom.Id;
                    Number.PhoneNumber = Request.Form[$"phoneNumber{i}"]!;
                    PhoneNums.Add(Number);
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < 100; i++)
            {
                if (Request.Form[$"email{i}"].ToString() != "")
                {
                    RoomEmail = new Email();
                    RoomEmail.QuestRoomId = QuestRoom.Id;
                    RoomEmail.EmailName = Request.Form[$"email{i}"]!;
                    Emails.Add(RoomEmail);
                }
                else
                {
                    break;
                }
            }

            if (files.Count > 1)
            {
                for (int i = 1; i < 100; i++)
                {
                    try
                    {
                        Picture = new PicPath();
                        Picture.QuestRoomId = QuestRoom.Id;
                        var gallery_dir = $@"{Directory.GetCurrentDirectory()}\wwwroot\Photos\{QuestRoom!.Name.Replace(" ", "_")}\Gallery";
                        Directory.CreateDirectory(gallery_dir);
                        Picture.Path = $@"{gallery_dir}\{files[i].FileName}";
                        using (FileStream fs = new FileStream(Picture.Path, FileMode.Create))
                        {
                            await files[i].CopyToAsync(fs);
                        }
                        Picture.Path = $@"{gallery_dir}\{files[i].FileName}".Split("wwwroot")[1];
                        PicPaths!.Add(Picture);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
            if (PicPaths!.Count > 0)
                await db.AddRangeAsync(PicPaths);
            await db.PhoneNums.AddRangeAsync(PhoneNums);
            await db.Emails.AddRangeAsync(Emails);

            await db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
