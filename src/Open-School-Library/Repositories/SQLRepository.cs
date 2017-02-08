using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Open_School_Library.Data;
using Open_School_Library.Data.Entities;
using Open_School_Library.Models.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Repositories
{
    public class SQLRepository : IBookRepository
    {
        private LibraryContext _context;

        public SQLRepository()
        {
        }

        public SQLRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Book> AddBook(Book newBook)
        {
            _context.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public async Task<Book> CreateBook(Book newBook)
        {
            _context.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public async Task<Book> DeleteBook(int? id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookID == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public BookCheckoutViewModel GetBookToCheckOut(int? id)
        {
            var bookToCheckOut =
            _context.Books
            .Where(b => b.BookID == id)
            .Select(r => new BookCheckoutViewModel
            {
                BookID = r.BookID,
                Title = r.Title

            }).FirstOrDefault();

            bookToCheckOut.Students = new SelectList(_context.Students.Select(s => new { s.StudentID, Name = $"{s.FirstName} {s.LastName}" }).ToList(), "StudentID", "Name");

            return bookToCheckOut;

        }

        public IEnumerable<BookIndexViewModel> GetAllBooks()
        {
            var books = from book in _context.Books
                        join loan in _context.BookLoans.Where(x => !x.ReturnedOn.HasValue) on book.BookID equals loan.BookID into result
                        from loanWithDefault in result.DefaultIfEmpty()
                        orderby book.Title
                        select new BookIndexViewModel
                        {
                            BookID = book.BookID,
                            Title = book.Title,
                            Author = book.Author,
                            ISBN = book.ISBN,
                            GenreName = book.Genre.Name,
                            IsAvailable = loanWithDefault == null,
                            AvailableOn = loanWithDefault == null ? (DateTime?)null : loanWithDefault.DueOn
                        };

            return books;
        }

        public BookDetailsViewModel GetBook(int? id)
        {
            var book = (from theBook in _context.Books.Where(b => b.BookID == id)
                        join loan in _context.BookLoans.Where(x => !x.ReturnedOn.HasValue) on theBook.BookID equals loan.BookID into result
                        from loanWithDefault in result.DefaultIfEmpty()
                        select new BookDetailsViewModel
                        {
                            BookID = theBook.BookID,
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

            return book;
        }

        public BookDeleteViewModel GetBookToDelete(int? id)
        {
            var book =
            _context.Books
            .Where(b => b.BookID == id)
            .Select(r => new BookDeleteViewModel
            {
                BookID = r.BookID,
                Title = r.Title,
                SubTitle = r.SubTitle,
                Author = r.Author,
                ISBN = r.ISBN,
                DeweyName = r.Dewey.Name,
                GenreName = r.Genre.Name
            }).FirstOrDefault();

            return book;
        }

        public BookEditViewModel GetBookToUpdate(int? id)
        {
            var book =
            _context.Books
            .Where(b => b.BookID == id)
            .Select(r => new BookEditViewModel
            {
                BookID = r.BookID,
                Title = r.Title,
                SubTitle = r.SubTitle,
                Author = r.Author,
                ISBN = r.ISBN,
                DeweyID = r.Dewey.DeweyID,
                GenreID = r.Genre.GenreId
            }).FirstOrDefault();

            return book;
        }

        public async Task<Book> UpdateBook(Book updatedBook)
        {
            _context.Update(updatedBook);
            await _context.SaveChangesAsync();

            return updatedBook;
        }
    }
}
