using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.TeacherViewModels
{
    public class TeacherEditViewModel
    {
        public int TeacherID { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required!")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "A Grade is Required!")]
        [Range(0, 99)]
        public int Grade { get; set; }
    }
}
