using ASP_0511;
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

    if (path == "/rooms" && method == "GET")
    {
        await getRooms(resp);
    }
    //create
    else if (path == "/create" && method == "POST")
    {
        await createRoom(req, resp);
    }
    //get
    else if (Regex.IsMatch(path, regFind) && method == "GET")
    {
        await findRoom(req, resp);
    }
    //update
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

async Task findRoom(HttpRequest request, HttpResponse response)
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
	Room? room = await hreq.ReadFromJsonAsync<Room>();

	if (room != null)
	{
		room.Id = Guid.NewGuid().ToString();
		rooms.Add(room);

		await hres.WriteAsJsonAsync(room);
	}
	else
	{
		hres.StatusCode = 400;
		await hres.WriteAsJsonAsync(new { message = "Error___" });
	}
}

async Task updRoom(HttpRequest hreq, HttpResponse hres)
{
	Room? room = await hreq.ReadFromJsonAsync<Room>();

	if (room != null)
	{
		Room? temp = rooms.FirstOrDefault(a => a.Id == room.Id);
		if (temp != null)
		{
			temp.Name = room.Name;
			temp.Info = room.Info;

			await hres.WriteAsJsonAsync<Room>(temp);
		}
		else
		{
            hres.StatusCode = 400;
			await hres.WriteAsJsonAsync(new { message = "Error___" });
		}
	}
}
async Task saveRooms()
{
	string jsonrooms = JsonConvert.SerializeObject(rooms);
	using (StreamWriter sw = new StreamWriter("rooms.json", false))
	{
		await sw.WriteLineAsync(jsonrooms);
	}
}
async Task deleteRoom(HttpRequest hreq, HttpResponse hres)
{
    rooms.Remove(rooms.Find(o => o.Id == hreq.Query["id"]));
    saveRooms();
    hres.Redirect("/", false);
    await hres.WriteAsJsonAsync(new { deleted = true });
}

app.Run();