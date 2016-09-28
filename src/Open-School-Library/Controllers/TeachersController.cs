using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Open_School_Library.Data;
using Open_School_Library.Data.Entities;
using Open_School_Library.Models.TeacherViewModels;

namespace Open_School_Library.Controllers
{
    public class TeachersController : Controller
    {
        private readonly LibraryContext _context;

        public TeachersController(LibraryContext context)
        {
            _context = context;    
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            IEnumerable<TeacherIndexViewModel> teachers =
            _context.Teachers
            .Select(r => new TeacherIndexViewModel
            {
                TeacherID = r.TeacherID,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Grade = r.Grade
            });


            return View(teachers);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher =
            _context.Teachers
            .Where(t => t.TeacherID == id)
            .Select(r => new TeacherDetailViewModel
            {
                TeacherID = r.TeacherID,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Grade = r.Grade
            }).FirstOrDefault();

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,FirstName,Grade,LastName")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher =
            _context.Teachers
            .Where(t => t.TeacherID == id)
            .Select(r => new TeacherEditViewModel
            {
                TeacherID = r.TeacherID,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Grade = r.Grade
            }).FirstOrDefault();

            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherID,FirstName,Grade,LastName")] Teacher teacher)
        {
            if (id != teacher.TeacherID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.TeacherID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher =
            _context.Teachers
            .Where(t => t.TeacherID == id)
            .Select(r => new TeacherDeleteViewModel
            {
                TeacherID = r.TeacherID,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Grade = r.Grade
            }).FirstOrDefault();

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.TeacherID == id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherID == id);
        }
    }
}
