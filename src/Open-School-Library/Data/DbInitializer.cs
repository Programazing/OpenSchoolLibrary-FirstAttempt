using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Open_School_Library.Data.Entities;

namespace Open_School_Library.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            /*The following is if you want to check befoe deleting
            this will be removed before version 1.0.0 beta release
            if you see this in the code after the release please
            remove it. */

            /*if(context.Students.Any())
            {
                return; //DB has been seeded
            }*/

            var teachers = new Teacher[]
            {
                new Teacher {FirstName="Erik2", LastName="Henderson", Grade=3 }
            };

            foreach (Teacher s in teachers)
            {
                context.Teachers.Add(s);
            }

            context.SaveChanges();

            var students = new Student[]
            {
                new Student { FirstName="John", LastName="Tell", Email="a@b.com", Grade=3, TeacherID = 1 , Fines=01.50M, IssuedID=10254 }
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }

            context.SaveChanges();

            var genres = new Genre[]
            {
                new Genre { Name="Horror" },
                new Genre { Name="Romance" },
                new Genre { Name="Blah " },
                new Genre { Name="Blah2" }
            };

            foreach (Genre s in genres)
            {
                context.Genres.Add(s);
            }

            context.SaveChanges();

            var deweys = new Dewey[]
            {
                new Dewey { Name="Computer Science, Information & General Works", Number=000 },
                new Dewey { Name="Philosophy & Psychology", Number=100 },
                new Dewey { Name="Religion", Number=200 }
            };

            foreach (Dewey s in deweys)
            {
                context.Deweys.Add(s);
            }

            context.SaveChanges();

            var books = new Book[]
            {
                new Book {Title="Moby Dick", SubTitle="", Author="Author Guy", ISBN=12345, GenreID=4, DeweyID=1  }
            };

            foreach (Book s in books)
            {
                context.Books.Add(s);
            }

            context.SaveChanges();

            var bookloans = new BookLoan[]
            {
                new BookLoan { BookID=1, StudentID=1, CheckedOutWhen= new DateTime(2016, 9, 21), DueWhen= new DateTime(2016, 10, 21) }
            };

            foreach (BookLoan s in bookloans)
            {
                context.BookLoans.Add(s);
            }

            context.SaveChanges();

            var settings = new Setting[]
            {
                new Setting { FineAmount=00.25M }
            };

            foreach (Setting s in settings)
            {
                context.Settings.Add(s);
            }

            context.SaveChanges();
        }

    }
}
