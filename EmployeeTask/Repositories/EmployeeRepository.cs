using EmployeeTask.Data;
using EmployeeTask.Models;
using EmployeeTask.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Employee>> Search()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Employee employee)
        {
            var emp = await Find(employee.Id);
            _db.Entry(employee).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
