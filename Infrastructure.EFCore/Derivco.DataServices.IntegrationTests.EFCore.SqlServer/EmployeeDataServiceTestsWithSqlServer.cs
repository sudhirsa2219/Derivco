using GamesGlobal.WebApi.DataServices.EFCore;
using GamesGlobal.WebApi.Domains.Entities;
using GamesGlobal.WebApi.Domains.TestData;
using GamesGlobal.WebApi.EFCore.Setup;
using GamesGlobal.WebApi.Test.Core.TestBases;
using Xunit;

namespace GamesGlobal.WebApi.DataServices.IntegrationTests.EFCore.SqlServer;


public class EmployeeDataServiceTestsWithSqlServer : DataServiceSqlServerBaseIntegrationTests<Employee, int>, IClassFixture<DatabaseSetup>
{
    public EmployeeDataServiceTestsWithSqlServer() : base(new EmployeeDataService(TestDbContextFactory.CreateSqlServerDbContext()), x => x.Id, new EmployeeDataFactory())
    {

    }

}
