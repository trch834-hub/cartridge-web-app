using Microsoft.EntityFrameworkCore;
using CartridgeWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ ДЛЯ RENDER - используем порт из переменной окружения
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddRazorPages();
builder.Services.AddControllers();

// ✅ БАЗА ДАННЫХ - для демо используем InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CartridgeDB"));

var app = builder.Build();

// ✅ НАСТРОЙКА СТАТИЧЕСКИХ ФАЙЛОВ
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

// ✅ ПРОСТОЙ MAP ДЛЯ ГЛАВНОЙ СТРАНИЦЫ
app.MapFallbackToFile("login.html"); // ← Это отправляет все запросы на login.html

// ✅ ТЕСТОВЫЕ ДАННЫЕ
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Cartridges.Any())
    {
        context.Cartridges.AddRange(
            new Cartridge { Id = 1, CartridgeModel = "HP 83X Black", PrinterId = 1, TotalQuantity = 5, BookedQuantity = 2 },
            new Cartridge { Id = 2, CartridgeModel = "HP 83X Color", PrinterId = 2, TotalQuantity = 3, BookedQuantity = 0 },
            new Cartridge { Id = 3, CartridgeModel = "Epson 79N Black", PrinterId = 3, TotalQuantity = 2, BookedQuantity = 1 }
        );
        context.SaveChanges();
    }

    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User { Id = 1, Username = "admin", Password = "12345", Role = "Admin" },
            new User { Id = 2, Username = "user1", Password = "11111", Role = "User" }
        );
        context.SaveChanges();
    }
}

app.Run();