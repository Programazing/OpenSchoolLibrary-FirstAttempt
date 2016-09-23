using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Open_School_Library.Models.DatabaseModels;

namespace Open_School_Library.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Dewey> Deweys { get; set; }
    }
}
