using EmployeeTask.Data;
using EmployeeTask.Models;
using EmployeeTask.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories
{
    public class DepartmentRepository:Repository<Department>,IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
    }
}
