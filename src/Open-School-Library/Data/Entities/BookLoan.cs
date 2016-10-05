using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Data.Entities
{
    public class BookLoan
    {
        public int BookLoanID { get; set; }
        public int BookID { get; set; }
        public int StudentID { get; set; }
        public DateTime CheckedOutOn { get; set; }
        public DateTime DueOn { get; set; }
        public DateTime? ReturnedOn { get; set; }

        public Book Book { get; set; }
        public Student Student { get; set; }
    }
}
