using EmployeeTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories.IRepositories
{
    public interface ISubSectionRepository : IRepository<SubSection>
    {
        Task<IEnumerable<SubSection>> GetAllBySectionId(int id);
    }
}
