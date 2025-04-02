using LogAnalizerServer;
using LogAnalizerServer.Data;

using LogAnalizerServer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LogAnalizerServerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<LogService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.Urls.Add("http://localhost:5000");


app.UseAuthorization();
app.MapControllers();

app.Run();