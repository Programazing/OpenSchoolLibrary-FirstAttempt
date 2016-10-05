using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Open_School_Library.Data;
using Open_School_Library.Data.Entities;
using Open_School_Library.Models.BookViewModels;

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

            var books = from book in _context.Books
                            join loan in _context.BookLoans.Where(x => !x.ReturnedOn.HasValue) on book.BookId equals loan.BookID into result
                            from loanWithDefault in result.DefaultIfEmpty()
                            select new BookIndexViewModel
                            {
                                BookId = book.BookId,
                                Title = book.Title,
                                Author= book.Author,
                                ISBN = book.ISBN,
                                GenreName = book.Genre.Name,
                                IsAvailable = loanWithDefault == null,
                                AvailableOn = loanWithDefault == null ? (DateTime?)null : loanWithDefault.DueOn
                            };

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =
            _context.Books
            .Where(b => b.BookId == id)
            .Select(r => new BookDetailsViewModel
            {
                BookID = r.BookId,
                Title = r.Title,
                SubTitle = r.SubTitle,
                Author = r.Author,
                ISBN = r.ISBN,
                DeweyName = r.Dewey.Name,
                GenreName = r.Genre.Name
            }).FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var model = new BookCreateViewModel();

            

            model.GenreList = new SelectList(_context.Genres.Select(b => new { b.GenreId, b.Name }).ToList(), "GenreId", "Name");
            model.DeweyList = new SelectList(_context.Deweys.Select(b => new { b.DeweyID, b.Name }).ToList(), "DeweyID", "Name");


            //ViewData["DeweyID"] = new SelectList(_context.Deweys, "DeweyID", "DeweyID");
            //ViewData["GenreID"] = new SelectList(_context.Genres, "GenreId", "GenreId");
            return View(model);
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
            .Where(b => b.BookId == id)
            .Select(r => new BookEditViewModel
            {
                BookID = r.BookId,
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
            if (id != book.BookId)
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
                    if (!BookExists(book.BookId))
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

            var book =
            _context.Books
            .Where(b => b.BookId == id)
            .Select(r => new BookDeleteViewModel
            {
                BookID = r.BookId,
                Title = r.Title,
                SubTitle = r.SubTitle,
                Author = r.Author,
                ISBN = r.ISBN,
                DeweyName = r.Dewey.Name,
                GenreName = r.Genre.Name
            }).FirstOrDefault();

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
            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookId == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            if(isBookCheckedOut(id) == false)
            {
                var bookloan =
                _context.Books
                .Where(b => b.BookId == id)
                .Select(r => new BookCheckoutViewModel
                {
                    BookID = r.BookId,
                    Title = r.Title

                }).FirstOrDefault();

               bookloan.Students = new SelectList(_context.Students.Select(s => new { s.StudentID, Name = $"{s.FirstName} {s.LastName}" }).ToList(), "StudentID", "Name");

                if (bookloan == null)
                {
                    return NotFound();
                }

                return View(bookloan);
            }
            else
            {
                ViewData["AlreadyCheckedOut"] = "This book has already been checked out.";
                return RedirectToAction("CheckedOut");
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(int BookID, int StudentID)
        {

            if (ModelState.IsValid && isBookCheckedOut(BookID) == false)
            {
                int thirtyDays =
                    _context.Settings
                    .Select(s => s.CheckoutDurationInDays)
                    .FirstOrDefault();


                var bookloan = new BookLoan()
                {
                    BookID = BookID,
                    StudentID = StudentID,
                    CheckedOutOn = DateTime.Now,
                    DueOn = DateTime.Now.AddDays(thirtyDays)

                };

                _context.Add(bookloan);
                await _context.SaveChangesAsync();
                ViewBag.SuccessfullyCheckedOut = "Successfully checked out!";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("CheckedOut");
            }

        }

        public ActionResult CheckedOut()
        {
            return View();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }

        private bool isBookCheckedOut(int? id)
        {
            var status = _context.BookLoans
                .Where(b => b.BookID == id && b.ReturnedOn == null)
                .FirstOrDefault();

            if(status == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
