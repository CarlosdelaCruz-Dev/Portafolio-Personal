using Microsoft.EntityFrameworkCore;
using PortafolioPersonal.Models;
using PortafolioPersonal.Data;

var builder = WebApplication.CreateBuilder(args);

// Esta línea le dice a .NET que añada las herramientas de Razor Pages al proyecto.
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache(); // Configura el cache para la sesión.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // El tiempo que la sesión puede estar inactiva.
    options.Cookie.HttpOnly = true; // Hace que la cookie solo sea accesible por el servidor.
    options.Cookie.IsEssential = true; // Hace que la cookie sea esencial para la aplicación.
});


// Esta línea es la que configura la conexión a la base de datos.
// 'AddDbContext' registra nuestra clase 'ApplicationDbContext'.
// 'options.UseSqlite' le dice que usaremos SQLite.
// 'GetConnectionString("DefaultConnection")' obtiene el enlace a tu base de datos
// que está guardado en el archivo 'appsettings.json'.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider; // Usamos el proveedor del ámbito temporal

    // Llamamos a nuestro método y le pasamos 'services', NO 'app.Services'
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
