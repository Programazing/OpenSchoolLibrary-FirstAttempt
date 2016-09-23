using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Open_School_Library.Models.DatabaseModels;

namespace Open_School_Library.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();

            //Look for Students
            if(context.Students.Any())
            {
                return; //DB has been seeded
            }

            var teachers = new Teacher[]
            {
                new Teacher {FirstName="Erik", LastName="Henderson", Grade=3 }
            };

            foreach (Teacher s in teachers)
            {
                context.Teachers.Add(s);
            }

            context.SaveChanges();

            var students = new Student[]
            {
                new Student { FirstName="John", LastName="Tell", Email="a@b.com", Grade=3, TeacherID = 1 , Fines=1.50M, IssusedID=10254 }
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }

            context.SaveChanges();

            var genres = new Genre[]
            {
                new Genre { Name="Horror" },
                new Genre { Name="Romance" }
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
        }

    }
}
