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
        }

        public IEmployeeRepository Employee { get; private set; }

        public IUserRepository User { get; private set; }
        public IUserManger UserManger { get; private set; }


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
