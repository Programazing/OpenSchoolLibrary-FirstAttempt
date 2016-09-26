using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Open_School_Library.Data;
using Open_School_Library.Models.DatabaseModels;
using Open_School_Library.Models;

namespace Open_School_Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;    
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Books.Include(b => b.Dewey).Include(b => b.Genre);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["DeweyID"] = new SelectList(_context.Deweys, "DeweyID", "DeweyID");
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreId", "GenreId");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Author,DeweyID,GenreID,ISBN,SubTitle,Title")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DeweyID"] = new SelectList(_context.Deweys, "DeweyID", "DeweyID", book.DeweyID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreId", "GenreId", book.GenreID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =
            _context.Books
            .Where(b => b.BookID == id)
            .Select(r => new BookViewModel
            {
                BookID = r.BookID,
                Title = r.Title,
                SubTitle = r.SubTitle,
                Author = r.Author,
                ISBN = r.ISBN,
                DeweyID = r.Dewey.DeweyID,
                GenreID = r.Genre.GenreId
            }).FirstOrDefault();

            book.GenreList = new SelectList(_context.Genres.Select(b => new { b.GenreId, b.Name}).ToList(), "GenreId", "Name");
            book.DeweyList = new SelectList(_context.Deweys.Select(b => new { b.DeweyID, b.Name }).ToList(), "DeweyID", "Name");

            if (book == null)
            {
                return NotFound();
            }
            
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Author,DeweyID,GenreID,ISBN,SubTitle,Title")] Book book)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
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
            ViewData["DeweyID"] = new SelectList(_context.Deweys, "DeweyID", "DeweyID", book.DeweyID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreId", "GenreId", book.GenreID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookID == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
