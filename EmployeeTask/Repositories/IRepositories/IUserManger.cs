using EmployeeTask.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories.IRepositories
{
    public interface IUserManger
    {
        Task SignIn( HttpContext httpContext,UserItem user,bool isPersistent = false);

        Task SignOut(HttpContext httpContext);
    }
}
