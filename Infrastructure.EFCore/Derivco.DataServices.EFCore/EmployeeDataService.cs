using Derivco.Core.DataService.EFCore;
using Derivco.DataServices.EFCore.DataContext;
using Derivco.DataServices.Interfaces;
using Derivco.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Derivco.DataServices.EFCore
{
    public class EmployeeDataService : EntityDataService<Employee>, IEmployeeDataService
    {
        public EmployeeDataService(AppDbContext dbContext) : base(dbContext)
        {

        }

        public virtual async Task<IList<Employee>> GetByFirstName(string firstName)
        {
            return await DbContext.Set<Employee>().Where(x => x.FirstName.Contains(firstName)).ToListAsync();
        }

    }
}
