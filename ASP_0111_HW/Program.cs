using ASP_0111_HW;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
List<Restaurant> restaurants = new List<Restaurant>()
{
	new Restaurant(){Id = Guid.NewGuid().ToString(), Name = "Rest1", Description = "Desc1" },
	new Restaurant(){Id = Guid.NewGuid().ToString(), Name = "Rest2", Description = "Desc2" },
	new Restaurant(){Id = Guid.NewGuid().ToString(), Name = "Rest3", Description = "Desc3" }
};
List<Country> countries = new List<Country>()
{
	new Country(){Id = Guid.NewGuid().ToString(), CountryName = "Kyiv", Capital = "Ukraine"},
	new Country(){Id = Guid.NewGuid().ToString(), CountryName = "Warsaw", Capital="Poland"},
	new Country(){Id = Guid.NewGuid().ToString(), CountryName = "Praha", Capital="CZ"},
	new Country(){Id = Guid.NewGuid().ToString(), CountryName = "Rome", Capital="Italy"}
};
//1
//app.MapGet("/", () => DateTime.Now.DayOfYear.ToString());

//2
/*Random rand = new Random();
app.MapGet("/", () => (char)rand.Next(65, 122));*/

//3-4
/*app.Run(async (context) =>
{
	var path = context.Request.Path;
	var meth = context.Request.Method;

	if (path == "/restaurant" && meth == "GET")
	{
		await getRestaurants(context.Response);
	}
	else
	{
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("HTMLs/restaurant.html");
    }
});*/

//5

app.Run(async (context) =>
{
	var path = context.Request.Path;
	var meth = context.Request.Method;

	if (path == "/countries" && meth == "GET")
	{
		await getCountries(context.Response);
	}
	else
	{
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("HTMLs/countries.html");
    }

});

app.Run();

async Task getCountries(HttpResponse hres)
{
	await hres.WriteAsJsonAsync(countries);
}
async Task getRestaurants(HttpResponse hres)
{
	await hres.WriteAsJsonAsync(restaurants);
}