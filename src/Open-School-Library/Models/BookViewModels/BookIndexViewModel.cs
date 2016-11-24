using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.BookViewModels
{
    public class BookIndexViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        [Display(Name = "Author")]
        public string Author { get; set; }
        [Display(Name = "Genre")]
        public string GenreName { get; set; }
        public int ISBN { get; set; }
        public string BookLoan { get; set; }
        public DateTime? AvailableOn { get; set; }
        [Display(Name = "Availability")]
        public bool IsAvailable { get; set; }
    }
}
