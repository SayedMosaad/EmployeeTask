using EmployeeTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<UserItem> Register(RegisterVm model);
        Task<UserItem> Validate(LoginVm model);
    }
}
