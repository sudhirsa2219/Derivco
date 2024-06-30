using Derivco.DataServices.EFCore.DataContext;

namespace Derivco.EFCore.Setup
{
    public static class TestDbContextFactory
    {
        public static AppDbContext CreateInMemoryDbContext()
        {
            return new InMemoryDbContext(true);
        }

        public static AppDbContext CreateSqlServerDbContext()
        {
            return new SqlServerDbContext("Server=(localdb)\\mssqllocaldb;Database=Derivco.webapi;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
