using Microsoft.AspNetCore.Mvc.Rendering;
using Open_School_Library.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.BookViewModels
{
    public class BookEditViewModel
    {
        public int BookID { get; set; }

        [Required(ErrorMessage = "Title is Required!")]
        public string Title { get; set; }

        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Author is Required!")]
        public string Author { get; set; }
        public int GenreID { get; set; }
        public SelectList GenreList { get; set; }
        public int DeweyID { get; set; }
        public SelectList DeweyList { get; set; }
        [Required(ErrorMessage = "ISBN is Required!")]
        [MinLength(10, ErrorMessage = "Minimum length must be 10 numbers."), MaxLength(13, ErrorMessage = "Maximum length must be 13 numbers.")]
        public int ISBN { get; set; }
        public string Availability { get; set; }
    }
}
