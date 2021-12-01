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
    public class SectionRepository : Repository<Section>,ISectionRepository
    {
        private readonly ApplicationDbContext _db;

        public SectionRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Section>> GetAllByDepartmentId(int id)
        {
            return await _db.Sections.Where(e => e.DepartmentId == id).ToListAsync();
        }
    }
}
