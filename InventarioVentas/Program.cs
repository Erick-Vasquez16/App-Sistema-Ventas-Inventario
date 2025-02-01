using InventarioVentas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración de OpenAI desde appsettings.json
builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("OpenAIClient", client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/");
});

//para la base de datos
builder.Services.AddDbContext<AppDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

//configuracion para la sesion
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options => {
        options.LoginPath = "/Acceso/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
// Clase para manejar la configuración de OpenAI
public class OpenAIOptions
{
    public string ApiKey { get; set; }
}