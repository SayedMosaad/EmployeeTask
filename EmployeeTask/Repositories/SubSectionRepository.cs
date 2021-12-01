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
    public class SubSectionRepository : Repository<SubSection>,ISubSectionRepository
    {
        private readonly ApplicationDbContext _db;

        public SubSectionRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<SubSection>> GetAllBySectionId(int id)
        {
            return await _db.SubSections.Where(e => e.SectionId == id).ToListAsync();
        }
    }
}
