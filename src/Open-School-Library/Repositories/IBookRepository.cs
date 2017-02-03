using Open_School_Library.Data.Entities;
using Open_School_Library.Models.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Repositories
{
    public interface IBookRepository
    {
        Task<Book> AddBook(Book newBook);

        IEnumerable<BookIndexViewModel> GetAllBooks();

        BookDetailsViewModel GetBook(int? id);

        BookEditViewModel GetBookToUpdate(int? id);

        Task<Book> UpdateBook(Book updatedBook);

        void DeleteBook(int id);

    }
}
