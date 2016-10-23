using Open_School_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Helpers
{
    public class FineCalculations
    {
        private static LibraryContext _context;

        public FineCalculations(LibraryContext context)
        {
            _context = context;
        }

        public static double CalculateFine(DateTime CheckedOutOn, DateTime ReturnedOn)
        {

            return 90;
        }

        public static bool areFinesEnabled()
        {
            bool checkFines = _context.Settings.Select(x => x.AreFinesEnabled).FirstOrDefault();

            return checkFines;
        }
    }
}
