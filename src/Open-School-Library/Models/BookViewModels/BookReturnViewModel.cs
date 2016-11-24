using System;
using System.ComponentModel.DataAnnotations;

namespace Open_School_Library.Models.BookViewModels
{
    public class BookReturnViewModel
    {
        public int BookLoanID { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Due Back On")]
        public DateTime? AvailableOn { get; set; }
        [Display(Name = "Checked Out On")]
        public DateTime? CheckedOutOn { get; set; }
        public bool IsAvailable { get; set; }
        public int? StudentID { get; set; }
        public string StudentFristName { get; set; }
        public string StudentLastName { get; set; }
        public double? Fines { get; set; }
    }
}
