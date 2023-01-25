using HW111;

List<Restaurant> restaurants = new List<Restaurant>()
{
    new Restaurant(){ Id=Guid.NewGuid().ToString(),Title="Le Bernardin", City="New York" },
    new Restaurant(){ Id=Guid.NewGuid().ToString(),Title="Nihonryori Ryugin", City="Tokyo" },
    new Restaurant(){ Id=Guid.NewGuid().ToString(),Title="Guy Savoy Monnaie de Paris", City="Paris" },
};
List<Country> countries = new List<Country>()
{
    new Country(){ Title="Ukraine", Capital="Kyiv"},
    new Country(){ Title="Germany", Capital="Berlin"},
    new Country(){ Title="France", Capital="Paris"},
    new Country(){ Title="Italy", Capital="Rome"},
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//task1
//app.MapGet("/", () => "Current day: "+DateTime.Now.DayOfYear);
//task2
//char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
//char letter = alphabet[new Random().Next(0, alphabet.Length)];
//app.MapGet("/", () => "Random letter: " + letter);
//task3-4///////////////////////////////////////////////////////////////
app.Run(async (context) =>
{
    var response = context.Response;

    var path=context.Request.Path;
    var request = context.Request;

    if (path== "/restaurants" && request.Method=="GET")
    {
        await GetRests(response);
    }
    else
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/restaurant.html");
    }
});

//task5
//app.Run(async (context) =>
//{
//    var response=context.Response;
    
//    var path=context.Request.Path;
//    var request = context.Request;

//    if (path== "/countries" && request.Method=="GET")
//    {
//        await GetCountries(response);
//    }
//    else
//    {
//        context.Response.ContentType = "text/html; charset=utf-8";
//        await response.SendFileAsync("html/countries.html");
//    }
    
//});

app.Run();

async Task GetCountries(HttpResponse httpResponse)
{
    await httpResponse.WriteAsJsonAsync(countries);
}
async Task GetRests(HttpResponse httpResponse)
{
    await httpResponse.WriteAsJsonAsync(restaurants);
}


