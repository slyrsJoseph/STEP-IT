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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LogAnalizerServerDbContext>();
    db.Database.Migrate();
   
}

app.Urls.Add("http://localhost:5001");


app.UseAuthorization();
app.MapControllers();

app.Run();