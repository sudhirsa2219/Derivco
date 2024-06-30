using Derivco.Core.DataService;
using Derivco.Domains.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Derivco.DataServices.Interfaces
{
    public interface IEmployeeDataService : IEntityDataService<Employee>
    {
        Task<IList<Employee>> GetByFirstName(string firstName);

    }
}
