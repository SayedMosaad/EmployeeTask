using EmployeeTask.Data;
using EmployeeTask.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await Find(id);
            dbSet.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<T> Find(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IQueryable<T> query = dbSet;
            
            return await query.ToListAsync();

        }
        public IQueryable<T> GetAllAsQueryable()
        {
            IQueryable<T> query = dbSet;
            
            return query.AsNoTracking();

        }

    }
}
