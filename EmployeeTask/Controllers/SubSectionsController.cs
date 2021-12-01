using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeTask.Data;
using EmployeeTask.Models;

namespace EmployeeTask.Controllers
{
    public class SubSectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubSectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubSections
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubSections.Include(s => s.Section);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSection = await _context.SubSections
                .Include(s => s.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subSection == null)
            {
                return NotFound();
            }

            return View(subSection);
        }

        // GET: SubSections/Create
        public IActionResult Create()
        {
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Name");
            return View();
        }

        // POST: SubSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,SectionId,Id")] SubSection subSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id", subSection.SectionId);
            return View(subSection);
        }

        // GET: SubSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSection = await _context.SubSections.FindAsync(id);
            if (subSection == null)
            {
                return NotFound();
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id", subSection.SectionId);
            return View(subSection);
        }

        // POST: SubSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,SectionId,Id")] SubSection subSection)
        {
            if (id != subSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubSectionExists(subSection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id", subSection.SectionId);
            return View(subSection);
        }

        // GET: SubSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSection = await _context.SubSections
                .Include(s => s.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subSection == null)
            {
                return NotFound();
            }

            return View(subSection);
        }

        // POST: SubSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subSection = await _context.SubSections.FindAsync(id);
            _context.SubSections.Remove(subSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubSectionExists(int id)
        {
            return _context.SubSections.Any(e => e.Id == id);
        }
    }
}
