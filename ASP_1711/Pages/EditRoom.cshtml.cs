using ASP_1711.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASP_1711.Pages
{
    public class EditRoomModel : PageModel
    {
        RoomsContext db;

        [BindProperty]
        public QuestRoom? QuestRoom { get; set; }

        public EditRoomModel(RoomsContext db)
        {
            this.db = db;
            QuestRoom = new QuestRoom();
        }

        public async Task OnGetAsync(int id)
        {
            QuestRoom = await db.QuestRooms.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var old = await db.QuestRooms.Where(o => o.Id.Equals(QuestRoom!.Id)).AsNoTracking().FirstAsync();
            if (QuestRoom!.LogoPath == null) QuestRoom.LogoPath = old.LogoPath;
            else
            {
                var files = Request.Form.Files;
                var dir = $@"{Directory.GetCurrentDirectory()}\wwwroot\Photos\{QuestRoom!.Name.Replace(" ", "_")}";
                Directory.CreateDirectory(dir);
                if (files.Count != 0)
                {
                    using (FileStream fs = new FileStream($@"{Directory.GetCurrentDirectory()}\wwwroot\{old.LogoPath}", FileMode.Truncate))
                    {
                        await fs.FlushAsync();
                    }
                    QuestRoom.LogoPath = $@"{dir}\{files[0].FileName}";
                    using (FileStream fs = new FileStream(QuestRoom.LogoPath, FileMode.Create))
                    {
                        await files[0].CopyToAsync(fs);
                    }
                    QuestRoom.LogoPath = $@"{dir}\{files[0].FileName}".Split("wwwroot")[1];
                }
            }
            db.QuestRooms.Update(QuestRoom!);

            await db.SaveChangesAsync();

            return RedirectToPage("Admin"); //�������� ����� �������, ���������� �� ����, �� � ���������� 7 ����� *�����* ����� ������ �� ��������� ��������, �� ����� � ������� ���� ����� �� � �� ���� ��� � ���� ������ �����, ���� �������� ���, ����� 03:43 �� �� ������� � ���� ���������� �� ��� ������ �� ������, � ���� ������� �����. �������, �� ������ emotinal damage. �Ѳ �ϲ���Ĳ��� �������², ��Ҳ� ������� ������ ��ʲ����� ������
        }
    }
}
