using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace uniform_player.Database
{
    public class UniformPlayerDbContextFactory: IDesignTimeDbContextFactory<UniformPlayerDbContext>
    {
        public UniformPlayerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniformPlayerDbContext>();
            return new UniformPlayerDbContext();
        }
    }
}
