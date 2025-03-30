using LogAnalizerServer;
using LogAnalizerServer.Data;

using LogAnalizerServer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Подключение к БД
builder.Services.AddDbContext<LogAnalizerServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем LogService и другие сервисы в DI
builder.Services.AddScoped<LogService>();

// Добавляем поддержку контроллеров (если решишь работать с API)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(); // при необходимости

var app = builder.Build();

// Используем middleware
app.UseAuthorization();
app.MapControllers();

app.Run();