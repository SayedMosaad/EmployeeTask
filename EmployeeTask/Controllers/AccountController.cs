using EmployeeTask.Models;
using EmployeeTask.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(
                this.User.Claims.ToDictionary(
                    x => x.Type, x => x.Value));
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user =await _unitOfWork.User.Validate(model);

            if (user == null) return View(model);
            await _unitOfWork.UserManger.SignIn(this.HttpContext, user, false);
            return LocalRedirect("~/Home/Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _unitOfWork.User.Register(model);

            await _unitOfWork.UserManger.SignIn(this.HttpContext, user, false);
            return LocalRedirect("~/Home/Index");
        }

        public async Task<IActionResult> Logout()
        {
            await _unitOfWork.UserManger.SignOut(this.HttpContext);
            return RedirectPermanent("~/Home/Index");
        }
    }

}
