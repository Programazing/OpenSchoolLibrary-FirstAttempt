using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.StudentViewModels
{
    public class StudentDetailsViewModel
    {
        public int StudentID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int Grade { get; set; }
        public decimal? Fines { get; set; }
        [Display(Name = "Issued ID")]
        public int? IssuedID { get; set; }
        public string Email { get; set; }
        [Display(Name = "Teacher")]
        public int TeacherID { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
    }
}
