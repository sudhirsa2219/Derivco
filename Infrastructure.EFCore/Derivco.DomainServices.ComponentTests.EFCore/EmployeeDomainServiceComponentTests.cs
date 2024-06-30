using Derivco.DataServices.EFCore;
using Derivco.Domains.Entities;
using Derivco.Domains.TestData;
using Derivco.EFCore.Setup;
using Derivco.Test.Core.TestBases;

namespace Derivco.DomainServices.ComponentTests.EFCore
{
    public class EmployeeDomainServiceComponentTests : DomainServiceBaseComponentTests<Employee, int>
    {
        public EmployeeDomainServiceComponentTests() :
            base(new EmployeeDomainService(Factory_DataService()), x => x.Id, new EmployeeDataFactory())
        {

        }

        static EmployeeDataService Factory_DataService()
        {
            EmployeeDataService employeeDataService = new EmployeeDataService(TestDbContextFactory.CreateInMemoryDbContext());

            return employeeDataService;
        }
    }
}
