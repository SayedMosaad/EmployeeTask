using EmployeeTask.Data;
using EmployeeTask.Models;
using EmployeeTask.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserItem> Register(RegisterVm model)
        {
            //using var hmc = new HMACSHA512();
            //var hashedPassword = hmc.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            var salt = Hasher.GenerateSalt();
            var hashedPassword = Hasher.GenerateHash(model.Password, salt);


            var user = new User
            {
                Id = Guid.NewGuid(),
                PasswordHash = hashedPassword.ToString(),
                Salt=salt,
                UserName=model.UserName,
                Name = model.Name,
                RoleId=model.RoleId,
                CreatedOnUTC = DateTime.UtcNow
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            var createduser = await _db.Users.Include(e=>e.Roles).FirstOrDefaultAsync(e => e.Id == user.Id);
            return new UserItem
            {
                UserId = user.Id,
                UserName = user.UserName,
                RoleId=user.RoleId,
                Role= createduser.Roles.Name,
                CreatedUtc = user.CreatedOnUTC
            };
        }

        public async Task<UserItem> Validate(LoginVm model)
        {
            var emailRecords = _db.Users.Where(x => x.UserName == model.UserName).Include(e=>e.Roles);

            var results = emailRecords.AsEnumerable()
            .Where(m => m.PasswordHash == Hasher.GenerateHash(model.Password, m.Salt))
            .Select(m => new UserItem
            {
                UserId = m.Id,
                UserName = m.UserName,
                RoleId=m.RoleId,
                Role=m.Roles.Name,
                CreatedUtc = m.CreatedOnUTC
            });

            return  results.FirstOrDefault();
        }
    }
}
