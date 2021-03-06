﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.BookViewModels
{
    public class BookDeleteViewModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Subtitle")]
        public string SubTitle { get; set; }
        public string Author { get; set; }
        [Display(Name = "Genre")]
        public string GenreName { get; set; }
        [Display(Name = "Dewey")]
        public string DeweyName { get; set; }
        public int ISBN { get; set; }
        public string Availability { get; set; }
    }
}
