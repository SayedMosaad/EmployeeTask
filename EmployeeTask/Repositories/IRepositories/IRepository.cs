using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Find(int id);
        Task<bool> Add(T entity);
        Task<bool> Delete(int id);
    }
}
