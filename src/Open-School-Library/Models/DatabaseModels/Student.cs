using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.DatabaseModels
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
        [Column(TypeName = "Money")]
        public decimal? Fines { get; set; }
        public int? IssuedID { get; set; }
        public string Email { get; set; }

        public int TeacherID { get; set; }


        public Teacher Teacher { get; set; }

    }
}
