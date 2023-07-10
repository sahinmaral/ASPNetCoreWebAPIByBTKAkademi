using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using StoreApp.Repositories.EFCore;

namespace StoreApp.WebAPI.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<StoreAppDbContext>
    {
        public StoreAppDbContext CreateDbContext(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<StoreAppDbContext>()
                                .UseSqlServer(configuration.GetConnectionString("SQLServerConnectionString"), prj => prj.MigrationsAssembly(typeof(Program).Assembly.FullName));

            return new StoreAppDbContext(builder.Options);
        }
    }
}
