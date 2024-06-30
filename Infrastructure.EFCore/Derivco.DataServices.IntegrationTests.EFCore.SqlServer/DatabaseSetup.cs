using GamesGlobal.WebApi.EFCore.Setup;
using System;

namespace GamesGlobal.WebApi.DataServices.IntegrationTests.EFCore.SqlServer;

public class DatabaseSetup : IDisposable
{
    public DatabaseSetup()
    {
        var db = TestDbContextFactory.CreateSqlServerDbContext();
        DbContextDataInitializer.Initialize(db);
    }

    public void Dispose()
    {
        // ... clean up test data from the database ...
    }

}
