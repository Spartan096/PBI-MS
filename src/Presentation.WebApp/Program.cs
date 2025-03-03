using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Infrastructure; // Asegúrate de que la ruta es correcta para tu contexto de ventas

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración de DbContext para clientes usando EF Core
builder.Services.AddDbContext<IM252ClientesDbContext>(options =>
{
    // Usa tu cadena de conexión aquí
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configuración de DbContext para ventas usando EF Core
builder.Services.AddDbContext<IM252VentaDbContext>(options =>
{
    // Usa la misma cadena de conexión o una diferente, si es necesario
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Crear la aplicación
var app = builder.Build();

// Configurar el HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
