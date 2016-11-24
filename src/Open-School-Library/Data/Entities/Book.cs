using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Data.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Author { get; set; }
        public int GenreID { get; set; }
        public int DeweyID { get; set; }
        public int ISBN { get; set; }

        public Genre Genre { get; set; }
        public Dewey Dewey { get; set; }
    }
}
