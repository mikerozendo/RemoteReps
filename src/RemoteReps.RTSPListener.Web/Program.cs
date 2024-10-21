using RemoteReps.RTSPListener.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);
var hubPath =
    $"{Environment.GetEnvironmentVariable("WebApiBaseRoute")}{Environment.GetEnvironmentVariable("WebApiHubPath")}";

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(new SignalRHubRepository(hubPath));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Streaming}/{action=Index}/{id?}");

app.Run();