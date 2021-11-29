using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository Employee { get; }
        public IUserRepository User { get; }
        public IUserManger UserManger { get; }
    }
}
