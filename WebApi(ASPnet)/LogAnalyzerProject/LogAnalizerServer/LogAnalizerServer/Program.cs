using LogAnalizerServer;
using LogAnalizerServer.Data;
using LogAnalizerServer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<LogAnalizerServerDbContext>(options =>
{
    options.UseSqlServer(DatabaseConnectionManager.CurrentConnectionString);
});


builder.Services.AddScoped<LogService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();




app.Urls.Add("http://localhost:5001");
app.UseAuthorization();
app.MapControllers();
app.Run();