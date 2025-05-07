using LogAnalizerServer.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LogAnalizerServer.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Collections.Generic;
using System.Linq;


[ApiController]
[Route("api/[controller]")]


public class DataBaseController : ControllerBase
{
    
    private readonly string _databaseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Databases");

        public DataBaseController()
        {
            if (!Directory.Exists(_databaseFolder))
                Directory.CreateDirectory(_databaseFolder);
        }
        
        
        private void EnsureDatabaseIsReady()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LogAnalizerServerDbContext>();
            optionsBuilder.UseSqlServer(DatabaseConnectionManager.CurrentConnectionString);

            using var context = new LogAnalizerServerDbContext(optionsBuilder.Options);
            context.Database.Migrate(); 
        }
        
        
        
        [HttpGet("list")]
        public ActionResult<List<string>> ListDatabases()
        {
            var dbNames = new List<string>();

            using (var connection = new SqlConnection("Server=JOSEPHPC\\MSSQLSERVER01;Trusted_Connection=True;TrustServerCertificate=True;"))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT name FROM sys.databases WHERE name NOT IN ('master','tempdb','model','msdb')", connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dbNames.Add(reader.GetString(0));
                }
            }

            return Ok(dbNames);
        }
        

   
        
        [HttpPost("create")]
        public ActionResult CreateDatabase(string dbName)
        {
            var connectionString = $"Server=JOSEPHPC\\MSSQLSERVER01;Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";
            DatabaseConnectionManager.SetConnectionString(connectionString);

            try
            {
                using (var connection = new SqlConnection("Server=JOSEPHPC\\MSSQLSERVER01;Trusted_Connection=True;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"IF DB_ID('{dbName}') IS NULL CREATE DATABASE [{dbName}]";
                    command.ExecuteNonQuery();
                }

                EnsureDatabaseIsReady();
                return Ok($"Data base  '{dbName}' created and ready for use");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while creating data base: {ex.Message}");
            }
        }
        
        
        
        
        
        
        
    
        
        
        [HttpPost("select")]
        public ActionResult SelectDatabase(string dbName)
        {
            var connectionString = $"Server=JOSEPHPC\\MSSQLSERVER01;Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";
            DatabaseConnectionManager.SetConnectionString(connectionString);

            EnsureDatabaseIsReady();
            return Ok($"Data base  '{dbName}' selected ");
        }
        
        
        [HttpDelete("delete")]
        public ActionResult DeleteDatabase(string dbName)
        {
            try
            {
                using (var connection = new SqlConnection("Server=JOSEPHPC\\MSSQLSERVER01;Trusted_Connection=True;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    var cmd = new SqlCommand($@"
                    ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE [{dbName}];", connection);

                    cmd.ExecuteNonQuery();
                }

                return Ok($"Data base '{dbName}' deleted ");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while deleting database: {ex.Message}");
            }
        }
        
        

       
    }

    
