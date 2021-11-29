using EmployeeTask.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Controllers
{
    //[Authorize]
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _unitOfWork.Employee.GetAll();
            return View(employees);
        }
        
    }
}
