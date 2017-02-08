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

        BookDeleteViewModel GetBookToDelete(int? id);

        BookEditViewModel GetBookToUpdate(int? id);

        BookCheckoutViewModel GetBookToCheckOut(int? id);

        Task<Book> UpdateBook(Book updatedBook);

        Task<Book> DeleteBook(int? id);

    }
}
