using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository Employee { get; }
        public IDepartmentRepository Department { get; }
        public ISectionRepository Section { get; }
        public ISubSectionRepository SubSection { get; }
        public IUserRepository User { get; }
        public IUserManger UserManger { get; }
        public IRoleRepository Role { get; }
    }
}
