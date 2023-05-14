using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using NLog.Web;
using uniform_player.Database.Entities;

namespace uniform_player.Database
{
    public class UniformPlayerDbContext : DbContext
    {
        public DbSet<ScenarioEntity> ScenarioEntity { get; set; } = default!;
        private static string? connectionString;

        private void SetConnectionString(bool IntegrationTests = false)
        {

            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json").Build();
            connectionString = config.GetConnectionString("Development") ?? throw new NullReferenceException("Невозможно получить путь к базе");
            
        }

        public UniformPlayerDbContext()
        {
            if (connectionString == null)
                SetConnectionString();
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public UniformPlayerDbContext(DbContextOptions options) : base(options)
        {
            if (connectionString == null)
                SetConnectionString();
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (String.IsNullOrEmpty(connectionString))
                    throw new ArgumentNullException("connectionString");
                optionsBuilder.UseNpgsql(connectionString)
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(
                        LoggerFactory.Create(
                                builder => builder.AddNLogWeb()
                                )
                        );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}

