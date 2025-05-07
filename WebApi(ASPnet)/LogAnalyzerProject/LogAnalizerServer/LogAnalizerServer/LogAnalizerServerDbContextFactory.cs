using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using LogAnalizerServer.Data;

namespace LogAnalizerServer
{
    public class LogAnalizerServerDbContextFactory : IDesignTimeDbContextFactory<LogAnalizerServerDbContext>
    {
        public LogAnalizerServerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LogAnalizerServerDbContext>();
            optionsBuilder.UseSqlServer("Server=JOSEPHPC\\MSSQLSERVER01;Database=TestTest2;Trusted_Connection=True;TrustServerCertificate=True;");

            return new LogAnalizerServerDbContext(optionsBuilder.Options);
        }
    }
}