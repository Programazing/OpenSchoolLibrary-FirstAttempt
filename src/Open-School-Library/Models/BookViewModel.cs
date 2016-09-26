using Microsoft.AspNetCore.Mvc.Rendering;
using Open_School_Library.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models
{
    public class BookViewModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Author { get; set; }
        public int GenreID { get; set; }
        public SelectList GenreList { get; set; }
        public int DeweyID { get; set; }
        public SelectList DeweyList { get; set; }
        public int ISBN { get; set; }
        public string Availability { get; set; }
    }
}
