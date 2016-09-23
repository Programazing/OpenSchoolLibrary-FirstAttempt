using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.DatabaseModels
{
    public class BookLoan
    {
        public int BookLoanID { get; set; }
        public int BookID { get; set; }
        public int StudentID { get; set; }
        public DateTime CheckedOutWhen { get; set; }
        public DateTime DueWhen { get; set; }
        public DateTime ReturnedWhen { get; set; }

        public Book Book { get; set; }
        public Student Student { get; set; }
    }
}
