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
            if(context.Genres.Any())
            {
                return; //DB has been seeded
            }

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
