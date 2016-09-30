using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.BookViewModels
{
    public class BookCheckoutViewModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Availability { get; set; }


        [Display(Name ="Student")]
        public int StudentID { get; set; }
        public SelectList Students { get; set; }
    }
}
