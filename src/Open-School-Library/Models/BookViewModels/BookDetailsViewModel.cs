using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.BookViewModels
{
    public class BookDetailsViewModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Author { get; set; }
        public string GenreName { get; set; }
        public string DeweyName { get; set; }    
        public int ISBN { get; set; }
        public string Availability { get; set; }
    }
}
