using Open_School_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Helpers
{
    public class FineCalculations
    {
        private LibraryContext _context;

        public FineCalculations(LibraryContext context)
        {
            _context = context;
        }

        public decimal CalculateFine(DateTime CheckedOutOn, DateTime ReturnedOn)
        {
            var daysCheckedOut = DateTime.Parse(ReturnedOn.ToString()).Subtract(DateTime.Parse(CheckedOutOn.ToString())).TotalDays;
            var checkedOutDuration = _context.Settings.Select(x => x.CheckoutDurationInDays).FirstOrDefault();
            var daysToFine = (int)daysCheckedOut - checkedOutDuration;
            var fineRate = _context.Settings.Select(x => x.FineAmountPerDay).FirstOrDefault();
            var fine = fineRate * (decimal)daysToFine;

            return (decimal)fine;
        }

        public bool areFinesEnabled()
        {
            bool checkFines = _context.Settings.Select(x => x.AreFinesEnabled).FirstOrDefault();

            if(checkFines == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReturnedLate (int BookLoanID)
        {
            var DueOn = _context.BookLoans.Where(x => x.BookLoanID == BookLoanID).Select(x => x.DueOn).FirstOrDefault();
            var isLate = DateTime.Now > DueOn.Date;

            return isLate;
        }
    }
}
