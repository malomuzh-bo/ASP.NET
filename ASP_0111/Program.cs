/*var builder = WebApplication.CreateBuilder();
var app = builder.Build();
app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    var stringBuilder = new System.Text.StringBuilder("<table>");

    foreach (var header in context.Request.Headers)
    {
        stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
    }
    stringBuilder.Append("</table>");
    await context.Response.WriteAsync(stringBuilder.ToString());
});
app.Run();*/
//1
/*var builder = WebApplication.CreateBuilder();
var app = builder.Build();
app.MapGet("/", () => "Wassup");
app.Run();*/
//2
/*var builder = WebApplication.CreateBuilder();
var app = builder.Build();
app.MapGet("/", () => "Now is: " + DateTime.Now);
app.Run();*/
//3-4
/*using ASP_0111;
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
User user = new User("Bohdan", "380968984183");
app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; chaset=utf-8";
    //1 варіант
    await context.Response.SendFileAsync("user1.json");
    //2 варіант
    //await context.Response.WriteAsJsonAsync(user);

});
app.Run();*/
//5
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";
    await response.SendFileAsync("HTMLs/quote.html");

});
app.Run();