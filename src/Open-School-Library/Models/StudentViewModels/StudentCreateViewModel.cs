using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.StudentViewModels
{
    public class StudentCreateViewModel
    {
        public int StudentID { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required!")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is Required!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Grade is Required!")]
        [Range(0, 99)]
        public int Grade { get; set; }
        public decimal? Fines { get; set; }
        [Display(Name = "Issued ID")]
        public int? IssuedID { get; set; }
        public string Email { get; set; }
        [Display(Name = "Teacher")]
        public int TeacherID { get; set; }
        public SelectList Teacher { get; set; }
    }
}
