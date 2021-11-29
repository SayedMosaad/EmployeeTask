using EmployeeTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories.IRepositories
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        Task<IEnumerable<Employee>> Search();
        Task<bool> Update(Employee employee);
    }
}
