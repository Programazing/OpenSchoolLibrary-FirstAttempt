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
using Open_School_Library.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Open_School_Library.Controllers
{
    [Authorize(Policy = "ElevatedRole")]
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;
        private readonly FineCalculations _finesCalculations;

        public BooksController(LibraryContext context, FineCalculations finesCalculations)
        {
            _context = context;
            _finesCalculations = finesCalculations;
        }

        [AllowAnonymous]
        // GET: Books
        public async Task<IActionResult> Index(string searchTerm, string option)
        {

            var books = from book in _context.Books
                            join loan in _context.BookLoans.Where(x => !x.ReturnedOn.HasValue) on book.BookId equals loan.BookID into result
                            from loanWithDefault in result.DefaultIfEmpty()
                            orderby book.Title
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

            if (!String.IsNullOrEmpty(searchTerm))
            {
                if (!String.IsNullOrEmpty(option))
                {
                    switch (option)
                    {
                        case "author":
                            books = books.Where(s => s.Author.Contains(searchTerm));
                            break;
                        case "isbn":
                            int isbn = Convert.ToInt32(searchTerm);
                            books = books.Where(s => s.ISBN == isbn);
                            break;
                        case "genre":
                            books = books.Where(s => s.GenreName.Contains(searchTerm));
                            break;
                        default:
                            books = books.Where(s => s.Title.Contains(searchTerm));
                            break;
                    }
                }
            }

            return View(await books.ToListAsync());
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = (from theBook in _context.Books.Where(b => b.BookId == id)
                        join loan in _context.BookLoans.Where(x => !x.ReturnedOn.HasValue) on theBook.BookId equals loan.BookID into result
                        from loanWithDefault in result.DefaultIfEmpty()
                        select new BookDetailsViewModel
                        {
                            BookID = theBook.BookId,
                            SubTitle = theBook.SubTitle,
                            Title = theBook.Title,
                            Author = theBook.Author,
                            ISBN = theBook.ISBN,
                            GenreName = theBook.Genre.Name,
                            DeweyName = theBook.Dewey.Name,
                            StudentID = loanWithDefault == null ? null : loanWithDefault.StudentID,
                            StudentFristName = loanWithDefault == null ? null : loanWithDefault.Student.FirstName,
                            StudentLastName = loanWithDefault == null ? null : loanWithDefault.Student.LastName,
                            CheckedOutOn = loanWithDefault == null ? (DateTime?)null : loanWithDefault.CheckedOutOn,
                            IsAvailable = loanWithDefault == null,
                            AvailableOn = loanWithDefault == null ? (DateTime?)null : loanWithDefault.DueOn
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
        public IActionResult Edit(int? id)
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

            book.GenreList = new SelectList(_context.Genres.Select(b => new { b.GenreId, b.Name }).ToList(), "GenreId", "Name");
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
        public IActionResult Delete(int? id)
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

        // GET: Books/Checkout/5
        [HttpGet]
        public IActionResult Checkout(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            if (isBookCheckedOut(id) == false)
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

        // POST: Books/Checkout/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(int BookID, int StudentID)
        {

            if (ModelState.IsValid && isBookCheckedOut(BookID) == false)
            {
                int CheckoutDuration =
                    _context.Settings
                    .Select(s => s.CheckoutDurationInDays)
                    .FirstOrDefault();


                var bookloan = new BookLoan()
                {
                    BookID = BookID,
                    StudentID = StudentID,
                    CheckedOutOn = DateTime.Now,
                    DueOn = DateTime.Now.AddDays(CheckoutDuration)

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

        [HttpGet]
        public IActionResult Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (isBookCheckedOut(id) == false)
            {
                //Not checked out
                return RedirectToAction("Index");
            }
            else
            {
                var bookloan = (from book in _context.Books.Where(b => b.BookId == id)
                                join loan in _context.BookLoans.Where(x => !x.ReturnedOn.HasValue) on book.BookId equals loan.BookID into result
                                from loanWithDefault in result.DefaultIfEmpty()
                                select new BookReturnViewModel
                                {
                                    BookLoanID = loanWithDefault.BookLoanID,
                                    BookID = book.BookId,
                                    Title = book.Title,
                                    StudentID = loanWithDefault == null ? null : loanWithDefault.StudentID,
                                    StudentFristName = loanWithDefault == null ? null : loanWithDefault.Student.FirstName,
                                    StudentLastName = loanWithDefault == null ? null : loanWithDefault.Student.LastName,
                                    //Fines
                                    CheckedOutOn = loanWithDefault == null ? (DateTime?)null : loanWithDefault.CheckedOutOn,
                                    IsAvailable = loanWithDefault == null,
                                    AvailableOn = loanWithDefault == null ? (DateTime?)null : loanWithDefault.DueOn
                                }).FirstOrDefault();

                if (bookloan == null)
                {
                    return NotFound();
                }

                return View(bookloan);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(BookReturnViewModel model)
        {
            if (ModelState.IsValid && isBookCheckedOut(model.BookID) == true)
            {
                var bookLoan = _context.BookLoans.Where(x => x.BookLoanID == model.BookLoanID).FirstOrDefault();

                if(bookLoan != null)
                {
                    if (_finesCalculations.areFinesEnabled() == true && _finesCalculations.ReturnedLate(model.BookLoanID) == true)
                    {
                        var fine = _finesCalculations.CalculateFine((DateTime)model.CheckedOutOn, DateTime.Now);

                        if(fine > 0)
                        {
                            var addFineToStudent = _context.Students.Where(x => x.StudentID == model.StudentID).FirstOrDefault();
                            addFineToStudent.Fines += fine;
                            try
                            {
                                _context.Update(addFineToStudent);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                //TODO Add logging and error handling. - Christopher
                            }
                        }
                        
                        
                    }

                    bookLoan.BookLoanID = model.BookLoanID;
                    bookLoan.BookID = model.BookID;
                    bookLoan.StudentID = model.StudentID;
                    bookLoan.CheckedOutOn = (DateTime)model.CheckedOutOn;
                    bookLoan.DueOn = (DateTime)model.AvailableOn;
                    bookLoan.ReturnedOn = DateTime.Now;

                    try
                    {
                        _context.Update(bookLoan);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        //TODO Add logging and error handling. - Christopher
                    }

                    return RedirectToAction("Index");
                }

            }
            else
            {

            }

            return View();
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
