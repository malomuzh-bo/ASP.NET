using ASP_0311;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.RegularExpressions;

List<Auto> cars = new List<Auto>()
{
	new Auto(){Id = Guid.NewGuid().ToString(), Color = "Gray", Mark = "Lexus", Model = "IS300", Year = 1998},
	new Auto(){Id = Guid.NewGuid().ToString(), Color = "White", Mark = "Nissan", Model = "Skyline R34", Year = 1999},
	new Auto(){Id = Guid.NewGuid().ToString(), Color = "Red", Mark = "Ferrari", Model = "Portofino", Year = 2017}
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run(async (context) =>
{
	var req = context.Request;
	var resp = context.Response;
	var path = req.Path;
	var reg_str = @"^/api/cars/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$/gm";

	if (path == "api/cars" && req.Method == "GET")
	{
		await getCars(resp);
	}
	else if (Regex.IsMatch(path, reg_str) && req.Method == "GET")
	{

	}
	else if (path == "/api/cars" && req.Method == "POST")
	{
		await createAuto(resp, req);
	}
	else if (path == "/api/cars" && req.Method == "PUT")
	{

	}
	else if (path == "/api/cars" && req.Method == "DELETE")
	{

	}
	else
	{
		resp.ContentType = "text/html; charset=utf-8";
		await resp.SendFileAsync("html/htmlpage.html");
	}
});

app.Run();

async Task getCars(HttpResponse hres)
{
	await hres.WriteAsJsonAsync(cars);
}
async Task createAuto(HttpResponse hres, HttpRequest hreq)
{
	Auto auto = await hreq.ReadFromJsonAsync<Auto>();

	if (auto != null)
	{
		auto.Id = Guid.NewGuid().ToString();
		cars.Add(auto);
		await hres.WriteAsJsonAsync(auto);
	}
	else
	{
		hres.StatusCode = 400;
		await hres.WriteAsJsonAsync(new { message = "Error___" });
	}
}
async Task updAuto(HttpResponse hres, HttpRequest hreq)
{
	Auto? auto = await hreq.ReadFromJsonAsync<Auto>();
	if (auto != null)
	{
		Auto? tmp = cars.FirstOrDefault(q => q.Id == auto.Id);
		if (tmp != null)
		{
			tmp.Mark = auto.Mark;
			tmp.Model = auto.Model;
			tmp.Year = auto.Year;
			tmp.Color = auto.Color;
			await hres.WriteAsJsonAsync(tmp);
		}
		else
		{
			hres.StatusCode = 400;
			await hres.WriteAsJsonAsync("Error! Updated car isn't found");
        }
    }
	else
	{
		hres.StatusCode = 400;
        await hres.WriteAsJsonAsync("Error! Updated car isn't found");
    }
}
