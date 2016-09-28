using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Open_School_Library.Data;
using Open_School_Library.Data.Entities;
using Open_School_Library.Models.DeweyViewModels;

namespace Open_School_Library.Controllers
{
    public class DeweysController : Controller
    {
        private readonly LibraryContext _context;

        public DeweysController(LibraryContext context)
        {
            _context = context;    
        }

        // GET: Deweys
        public async Task<IActionResult> Index()
        {
            IEnumerable<DeweyIndexViewModel> deweys =
            _context.Deweys
            .Select(r => new DeweyIndexViewModel
            {
                DeweyID = r.DeweyID,
                Name = r.Name,
                Number = r.Number
            });

            return View(deweys);
        }

        // GET: Deweys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dewey =
            _context.Deweys
            .Where(d => d.DeweyID == id)
            .Select(r => new DeweyDetailsViewMode
            {
                DeweyID = r.DeweyID,
                Name = r.Name,
                Number = r.Number

            }).FirstOrDefault();

            if (dewey == null)
            {
                return NotFound();
            }

            return View(dewey);
        }

        // GET: Deweys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deweys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeweyID,Name,Number")] Dewey dewey)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dewey);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dewey);
        }

        // GET: Deweys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dewey =
            _context.Deweys
            .Where(d => d.DeweyID == id)
            .Select(r => new DeweyEditViewModel
            {
                DeweyID = r.DeweyID,
                Name = r.Name,
                Number = r.Number

            }).FirstOrDefault();

            if (dewey == null)
            {
                return NotFound();
            }
            return View(dewey);
        }

        // POST: Deweys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeweyID,Name,Number")] Dewey dewey)
        {
            if (id != dewey.DeweyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dewey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeweyExists(dewey.DeweyID))
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
            return View(dewey);
        }

        // GET: Deweys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dewey =
            _context.Deweys
            .Where(d => d.DeweyID == id)
            .Select(r => new DeweyDeleteViewModel
            {
                DeweyID = r.DeweyID,
                Name = r.Name,
                Number = r.Number

            }).FirstOrDefault();

            if (dewey == null)
            {
                return NotFound();
            }

            return View(dewey);
        }

        // POST: Deweys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dewey = await _context.Deweys.SingleOrDefaultAsync(m => m.DeweyID == id);
            _context.Deweys.Remove(dewey);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DeweyExists(int id)
        {
            return _context.Deweys.Any(e => e.DeweyID == id);
        }
    }
}
