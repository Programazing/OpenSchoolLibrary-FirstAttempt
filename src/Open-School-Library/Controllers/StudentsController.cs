using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Open_School_Library.Data;
using Open_School_Library.Data.Entities;
using Open_School_Library.Models.StudentViewModels;

namespace Open_School_Library.Controllers
{
    public class StudentsController : Controller
    {
        private readonly LibraryContext _context;

        public StudentsController(LibraryContext context)
        {
            _context = context;    
        }

        // GET: Students
        public async Task<IActionResult> Index(string searchTerm, string option)
        {
            IEnumerable<StudentIndexViewModel> students =
            _context.Students
            .Select(r => new StudentIndexViewModel
            {
                StudentID = r.StudentID,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Grade = r.Grade,
                Fines = r.Fines,
                IssuedID = r.IssuedID,
                Email = r.Email,
                TeacherID = r.TeacherID,
                TeacherFirstName = r.Teacher.FirstName,
                TeacherLastName = r.Teacher.LastName
            });

            if (!String.IsNullOrEmpty(searchTerm))
            {
                if (!String.IsNullOrEmpty(option))
                {
                    switch (option)
                    {
                        case "lname":
                            students = students.Where(s => s.LastName.Contains(searchTerm));
                            break;
                        case "grade":
                            int grade = Convert.ToInt32(searchTerm);
                            students = students.Where(s => s.Grade == grade);
                            break;
                        case "issuedID":
                            int issuedID = Convert.ToInt32(searchTerm);
                            students = students.Where(s => s.IssuedID == issuedID);
                            break;
                        case "email":
                            students = students.Where(s => s.Email.Contains(searchTerm));
                            break;
                        default:
                            students = students.Where(s => s.FirstName.Contains(searchTerm));
                            break;
                    }
                }
            }

            return View(students);

        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student =
            _context.Students
            .Where(s => s.StudentID == id)
            .Select(r => new StudentDetailsViewModel
            {

                StudentID = r.StudentID,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Grade = r.Grade,
                Fines = r.Fines,
                IssuedID = r.IssuedID,
                Email = r.Email,
                TeacherID = r.TeacherID,
                TeacherFirstName = r.Teacher.FirstName,
                TeacherLastName = r.Teacher.LastName

            }).FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var student = new StudentCreateViewModel();
            student.Teacher = new SelectList(_context.Students.Select(s => new { s.TeacherID, Name = $"{s.Teacher.FirstName} {s.Teacher.LastName}" }).ToList(), "TeacherID", "Name");

            return View(student);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,Email,Fines,FirstName,Grade,IssuedID,LastName,TeacherID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "TeacherID", student.TeacherID);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student =
            _context.Students
            .Where(s => s.StudentID == id)
            .Select(r => new StudentEditViewModel
            {

                StudentID= r.StudentID,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Grade = r.Grade,
                Fines = r.Fines,
                IssuedID = r.IssuedID,
                Email = r.Email,
                TeacherID = r.TeacherID

            }).FirstOrDefault();

            student.Teacher = new SelectList(_context.Students.Select(s => new { s.TeacherID, Name = $"{s.Teacher.FirstName} {s.Teacher.LastName}" }).ToList(), "TeacherID", "Name");

            if (student == null)
            {
                return NotFound();
            }
            //ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "TeacherID", student.TeacherID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,Email,Fines,FirstName,Grade,IssuedID,LastName,TeacherID")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "TeacherID", student.TeacherID);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student =
            _context.Students
            .Where(s => s.StudentID == id)
            .Select(r => new StudentDeleteViewModel
            {

                StudentID = r.StudentID,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Grade = r.Grade,
                Fines = r.Fines,
                IssuedID = r.IssuedID,
                Email = r.Email,
                TeacherID = r.TeacherID,
                TeacherFirstName = r.Teacher.FirstName,
                TeacherLastName = r.Teacher.LastName

            }).FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.StudentID == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}
