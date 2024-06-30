using Derivco.Core.WebApi;
using Derivco.Domains.Entities;
using Derivco.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace Derivco.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : WebApiControllerBase<Employee, int>
    {
        public EmployeesController(EmployeeDomainService employeeDomainService) : base(employeeDomainService)
        {

        }
    }
}
