using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.BookViewModels
{
    public class BookCreateViewModel
    {
        public int BookID { get; set; }
        [Required(ErrorMessage = "Title is Required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Subtitle is Required!")]
        [Display(Name = "Subtitle")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Author is Required!")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Genre is Required!")]
        [Display(Name = "Genre")]
        public int GenreID { get; set; }
        [Required(ErrorMessage = "Dewey is Required!")]
        [Display(Name = "Dewey")]
        public int DeweyID { get; set; }
        [MaxLength(13), MinLength(10)]
        public int ISBN { get; set; }

        public SelectList GenreList { get; set; }
        public SelectList DeweyList { get; set; }
    }
}
