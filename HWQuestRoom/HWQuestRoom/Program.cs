
using HWQuestRoom;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Text.RegularExpressions;

List<Room> rooms=new List<Room>()
{
    new Room(){ Id=Guid.NewGuid().ToString(), Title="Room1", RoomPath=""},
    new Room(){ Id=Guid.NewGuid().ToString(), Title="Room2", RoomPath=""},
    new Room(){ Id=Guid.NewGuid().ToString(), Title="Room3", RoomPath=""},
    new Room(){ Id=Guid.NewGuid().ToString(), Title="Room4", RoomPath=""},
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();
app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var path=request.Path;
    var regStr = @"^/api/rooms/\w$";
    response.ContentType = "text/html; charset=utf-8";

    //view
    if(path=="/api/rooms" && request.Method == "GET")
    {
        await GetRooms(response);
    }
    //get by index
    else if(Regex.IsMatch(path, regStr) && request.Method == "GET")
    {
        string? id = path.Value?.Split('/')[3];
        await GetByIndex(id, response);
    }
    //add
    else if (path == "/api/rooms" && request.Method == "POST")
    {
        //IFormFileCollection files = request.Form.Files;
        //var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        //string fullPath = "";
        //Directory.CreateDirectory(uploadPath);
        //foreach (var file in files)
        //{
        //    fullPath = $"{uploadPath}/{file.FileName}";
        //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }
        //}
        await CreateRoom(response, request);
    }
    //update
    else if(path=="/api/rooms" && request.Method == "PUT")
    {
        await UpdateRoom(response, request);
    }
    //del
    else if(path=="/api/rooms" && request.Method == "DELETE")
    {
        string? id=path.Value?.Split('/')[3];
        await DelRoom(id, response);
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});

async Task GetRooms(HttpResponse httpResponse)
{
    await httpResponse.WriteAsJsonAsync(rooms);
}
async Task GetByIndex(string? id, HttpResponse httpResponse)
{
    Room? room = rooms.FirstOrDefault(a => a.Id == id);
    Console.WriteLine(room);

    if (room != null)
    {
        await httpResponse.WriteAsJsonAsync(room);
    }
    else
    {
        httpResponse.StatusCode = 400;
        await httpResponse.WriteAsJsonAsync(new { message = "Error!" });
    }
}
async Task CreateRoom(HttpResponse httpResponse, HttpRequest httpRequest)
{
    Room? room = await httpRequest.ReadFromJsonAsync<Room>();

    if (room != null)
    {
        room.Id = Guid.NewGuid().ToString();
        rooms.Add(room);

        await httpResponse.WriteAsJsonAsync(room);
    }
    else
    {
        httpResponse.StatusCode = 400;
        await httpResponse.WriteAsJsonAsync(new { message = "Error!" });
    }
}

async Task UpdateRoom(HttpResponse httpResponse, HttpRequest httpRequest)
{
    Room? room = await httpRequest.ReadFromJsonAsync<Room>();

    if (room != null)
    {
        Room? temp = rooms.FirstOrDefault(a => a.Id == room.Id);
        if (temp != null)
        {
            temp.Title = room.Title;
            temp.RoomPath = room.RoomPath;

            await httpResponse.WriteAsJsonAsync<Room>(temp);
        }
        else
        {
            httpResponse.StatusCode = 400;
            await httpResponse.WriteAsJsonAsync(new { message = "Error!" });
        }
    }
}
async Task DelRoom(string? id, HttpResponse httpResponse)
{
    Room? temp = rooms.FirstOrDefault(a => a.Id == id);

    if (temp != null)
    {
        rooms.Remove(temp);

        await httpResponse.WriteAsJsonAsync(temp);
    }
    else
    {
        httpResponse.StatusCode = 400;
        await httpResponse.WriteAsJsonAsync(new { message = "Error!" });
    }
}

app.Run();
