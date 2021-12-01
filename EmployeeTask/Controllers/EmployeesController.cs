using EmployeeTask.Data;
using EmployeeTask.Models;
using EmployeeTask.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public EmployeesController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public async Task<IActionResult> Index(int DepartmentId, int SectionId, int SubSectionId, string Name)
        {
            var employees = Enumerable.Empty<Employee>();
           if(string.IsNullOrEmpty(Name))
            {
                employees = await _unitOfWork.Employee.GetAllAsQueryable()
                .Include(e => e.SubSection).ThenInclude(e => e.Section).ThenInclude(e => e.Department)
                .Where(e => ((DepartmentId == 0 || e.SubSection.Section.DepartmentId == DepartmentId)
                            && (SectionId == 0 || e.SubSection.SectionId == SectionId)
                            && (SubSectionId == 0 || e.SubSectionId == SubSectionId))).ToListAsync();
            }
           else
            {
                employees = await _unitOfWork.Employee.GetAllAsQueryable()
                    .Include(e => e.SubSection).ThenInclude(e => e.Section).ThenInclude(e => e.Department)
                    .Where(e => ((e.Name.Contains(Name))
                                &&(DepartmentId == 0 || e.SubSection.Section.DepartmentId == DepartmentId)
                                && (SectionId == 0 || e.SubSection.SectionId == SectionId)
                                && (SubSectionId == 0 || e.SubSectionId == SubSectionId))).ToListAsync();
            }
            var model = new EmployeeVm
            {
                Employees = employees
            };
            ViewData["DepartmentId"] = new SelectList(await _unitOfWork.Department.GetAll(), "Id", "Name");
            ViewData["SectionId"] = new SelectList(await _unitOfWork.Section.GetAll(), "Id", "Name");
            ViewData["SubSectionId"] = new SelectList(await _unitOfWork.SubSection.GetAll(), "Id", "Name");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["DepartmentId"] = new SelectList(await _unitOfWork.Department.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVm model)
        {
            if(ModelState.IsValid)
            {

                var result=await _unitOfWork.Employee.Add(model.Employee);
                if(result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DepartmentId"] = new SelectList(await _unitOfWork.Department.GetAll(), "Id", "Name");
            ViewData["SectionId"] = new SelectList(await _unitOfWork.Section.GetAll(), "Id", "Name");
            ViewData["SubSectionId"] = new SelectList(await _unitOfWork.SubSection.GetAll(), "Id", "Name");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _unitOfWork.Employee.GetAllAsQueryable().Where(e=>e.Id==id).Include(e=>e.SubSection).AsNoTracking().FirstOrDefaultAsync();
            if(employee==null)
            {
                return NotFound();
            }
            var model = new EmployeeVm
            {
                Employee=employee
            };
            ViewData["DepartmentId"] = new SelectList(await _unitOfWork.Department.GetAll(), "Id", "Name");
            ViewData["SectionId"] = new SelectList(await _unitOfWork.Section.GetAll(), "Id", "Name");
            ViewData["SubSectionId"] = new SelectList(await _unitOfWork.SubSection.GetAll(), "Id", "Name", employee.SubSectionId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeVm model)
        {
            if(ModelState.IsValid)
            {
                await _unitOfWork.Employee.Update(model.Employee);
                    return RedirectToAction("index");
            }
            ViewData["DepartmentId"] = new SelectList(await _unitOfWork.Department.GetAll(), "Id", "Name", model.DepartmentId);
            ViewData["SectionId"] = new SelectList(await _unitOfWork.Section.GetAll(), "Id", "Name", model.SectionId);
            ViewData["SubSectionId"] = new SelectList(await _unitOfWork.SubSection.GetAll(), "Id", "Name", model.Employee.SubSectionId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> JsonDelete(int id)
        {
            var employee = await _unitOfWork.Employee.Find(id);
            if (employee == null)
                return NotFound();
            await _unitOfWork.Employee.Delete(id);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await _unitOfWork.Employee.GetAllAsQueryable().Where(e => e.Id == id).Include(e => e.SubSection).ThenInclude(e => e.Section).ThenInclude(e => e.Department).AsNoTracking().FirstOrDefaultAsync();
            var model = new EmployeeVm
            {
                Employee = employee,
                DepartmentId=employee.SubSection.Section.DepartmentId,
                SectionId=employee.SubSection.SectionId
                
            };
            ViewData["DepartmentId"] = new SelectList(await _unitOfWork.Department.GetAll(), "Id", "Name",model.DepartmentId);
            ViewData["SectionId"] = new SelectList(await _unitOfWork.Section.GetAllAsQueryable().Where(e=>e.DepartmentId==model.DepartmentId).ToListAsync(), "Id", "Name",model.SectionId);
            ViewData["SubSectionId"] = new SelectList(await _unitOfWork.SubSection.GetAllAsQueryable().Where(e=>e.SectionId==model.SectionId).ToListAsync(), "Id", "Name",model.Employee.SubSectionId);
            return PartialView("_editView", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetSections(int id)
        {
            var Sections = new SelectList(await _unitOfWork.Section.GetAllByDepartmentId(id),"Id","Name");
            return Json(Sections);
        }

        [HttpPost]
        public async Task<IActionResult> GetSubSections(int id)
        {
            var SubSections = new SelectList(await _unitOfWork.SubSection.GetAllBySectionId(id),"Id","Name");
            return Json(SubSections);
        }

    }
}
