using Derivco.Core.DomainService;
using Derivco.DataServices.Interfaces;
using Derivco.Domains.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Derivco.DomainServices
{
    public class EmployeeDomainService : DomainService<Employee, int>
    {
        private readonly IEmployeeDataService _employeeDataService;

        public EmployeeDomainService(IEmployeeDataService employeeDataService) : base(employeeDataService)
        {
            _employeeDataService = employeeDataService;
        }

        public virtual async Task<IList<Employee>> GetByFirstName(string firstName)
        {
            return await _employeeDataService.GetByFirstName(firstName);
        }

    }
}
