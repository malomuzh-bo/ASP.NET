using Lab311;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Text.RegularExpressions;

List<Auto> autos = 
    new List<Auto>() { new Auto() {Id=Guid.NewGuid().ToString(),Color="color", Mark="mark", Model="model", Year=2011 },
    new Auto() {Id=Guid.NewGuid().ToString(),Color="color2", Mark="mark2", Model="model2", Year=2012 },
    new Auto() {Id=Guid.NewGuid().ToString(),Color="color3", Mark="mark3", Model="model3", Year=2013 }
    };

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.Run(async (context) =>{
    var request=context.Request;
    var path=request.Path;
    var response=context.Response;
    var regStr = @"^/api/autos/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$/gm";

    //view all
    if (path=="/api/autos" && request.Method=="GET")
    {
        await GetAutos(response);
    }
    //get by index
    else if (Regex.IsMatch(path, regStr) && request.Method == "GET")
    {

    }
    //add
    else if (path=="/api/autos" && request.Method=="POST")
    {
       await CreateAuto(response,request);
    }
    //update
    else if (path=="/api/autos" && request.Method=="PUT")
    {

    }
    //del
    else if(path=="/api/autos" && request.Method == "DELETE")
    {

    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();

async Task GetAutos(HttpResponse httpResponse)
{
    await httpResponse.WriteAsJsonAsync(autos);
}
async Task CreateAuto(HttpResponse httpResponse, HttpRequest httpRequest){
    Auto auto = await httpRequest.ReadFromJsonAsync<Auto>();

    if (auto != null)
    {
        auto.Id = Guid.NewGuid().ToString();
        autos.Add(auto);

        await httpResponse.WriteAsJsonAsync(auto);
    }
    else
    {
        httpResponse.StatusCode = 400;
        await httpResponse.WriteAsJsonAsync(new {message="Error!"});
    }
}
