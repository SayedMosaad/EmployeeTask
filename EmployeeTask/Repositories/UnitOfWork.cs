using EmployeeTask.Data;
using EmployeeTask.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Employee = new EmployeeRepository(_db);
            User = new UserRepository(_db);
            UserManger = new UserManager();
            Role = new RoleRepository(_db);
            Department = new DepartmentRepository(_db);
            Section = new SectionRepository(_db);
            SubSection = new SubSectionRepository(_db);
        }

        public IEmployeeRepository Employee { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public ISectionRepository Section { get; private set; }
        public ISubSectionRepository SubSection { get; private set; }

        public IUserRepository User { get; private set; }
        public IUserManger UserManger { get; private set; }
        public IRoleRepository Role { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
