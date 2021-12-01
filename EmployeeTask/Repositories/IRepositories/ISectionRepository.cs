using EmployeeTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories.IRepositories
{
    public interface ISectionRepository : IRepository<Section>
    {
        Task<IEnumerable<Section>> GetAllByDepartmentId(int id);
    }
}
