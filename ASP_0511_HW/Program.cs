using ASP_0511_HW;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

List<Room> rooms = new List<Room>()
{
	new Room(){Id = Guid.NewGuid().ToString(), Name = "Room1", Info = "Your"},
	new Room(){Id = Guid.NewGuid().ToString(), Name = "Room2", Info = "Your ad"},
	new Room(){Id = Guid.NewGuid().ToString(), Name = "Room3", Info = "Your ad could"},
	new Room(){Id = Guid.NewGuid().ToString(), Name = "Room4", Info = "Your ad could be"},
	new Room(){Id = Guid.NewGuid().ToString(), Name = "Room5", Info = "Your ad could be here"}
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
	var resp = context.Response;
	var req = context.Request;
	var path = req.Path;
	var method = context.Request.Method;
	var regFind = @"/find/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}";
	var regDelete = @"/delete/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}";
	resp.ContentType = "text/html; charset=utf-8";

	//getRooms
	if (path == "/rooms" && method == "GET")
	{
		await getRooms(resp);
	}
	//createRoom
	else if (path == "/create" && method == "POST")
	{
		await createRoom(req, resp);
	}
	//findRoom
	else if (Regex.IsMatch(path, regFind) && method == "GET")
	{
		await searchRoom(req, resp);
	}
	//updRoom
	else if (path == "/update" && method == "POST")
	{
		await updRoom(req, resp);
	}
	//delete
	else if (Regex.IsMatch(path, regDelete) && method == "DELETE")
	{
		await delRoom(req, resp);
	}
	else
	{
		resp.ContentType = "text/html";
		await resp.SendFileAsync("html/index.html");
	}
});

async Task getRooms(HttpResponse hr)
{
	await hr.WriteAsJsonAsync(rooms);
}

async Task searchRoom(HttpRequest request, HttpResponse response)
{
	await response.WriteAsJsonAsync(rooms.Find(o => o.Id == request.Query["id"]));
}

async Task getById(string id, HttpResponse hr)
{
	Room room = rooms.Where(q => q.Id == id).First();
	Console.WriteLine(room.ToString());

	if (room != null)
	{
		await hr.WriteAsJsonAsync(room);
	}
	else
	{
		hr.StatusCode = 400;
		await hr.WriteAsJsonAsync(new { message = "Error___" });
	}
}

async Task createRoom(HttpRequest hreq, HttpResponse hres)
{
	Room room = new Room() { Name = hreq.Form["name"], Info = hreq.Form["info"], Id = Guid.NewGuid().ToString() };
	var directory = $@"{Directory.GetCurrentDirectory()}\images\{room.Name}";
	Directory.CreateDirectory(directory);

	if (hreq.Form.Files.Count != 0)
	{
		room.Image = $@"{directory}\{hreq.Form.Files[0].FileName}";
		using (var fileStream = new FileStream(room.Image, FileMode.Create))
			await hreq.Form.Files[0].CopyToAsync(fileStream);
	}
	else
		room.Image = null;

	rooms.Add(room);
	hres.Redirect("/", false);
	save();
}

async Task updRoom(HttpRequest request, HttpResponse response)
{
	rooms.Find(o => o.Id == request.Form["id"])!.Name = request.Form["name"];
	rooms.Find(o => o.Id == request.Form["id"])!.Info = request.Form["info"];

	var directory = $@"{Directory.GetCurrentDirectory()}\images\{rooms.Find(o => o.Id == request.Form["id"])!.Name}";
	Directory.CreateDirectory(directory);

	if (request.Form.Files.Count != 0)
	{
		rooms.Find(o => o.Id == request.Form["id"])!.Image = $@"{directory}\{request.Form.Files[0].FileName}";
		using (var fileStream = new FileStream(rooms.Find(o => o.Id == request.Form["id"])!.Image, FileMode.Create))
			await request.Form.Files[0].CopyToAsync(fileStream);
	}
	response.Redirect("/", false);
	save();
}
async Task save()
{
	string jsonrooms = JsonConvert.SerializeObject(rooms);
	using (StreamWriter sw = new StreamWriter("rooms.json", false))
	{
		await sw.WriteLineAsync(jsonrooms);
	}
}
async Task delRoom(HttpRequest hreq, HttpResponse hres)
{
	rooms.Remove(rooms.Find(o => o.Id == hreq.Query["id"]));
	save();
	hres.Redirect("/", false);
	await hres.WriteAsJsonAsync(new { deleted = true });
}

app.Run();