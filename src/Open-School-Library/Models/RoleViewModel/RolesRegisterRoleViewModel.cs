using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.RoleViewModel
{
    public class RolesRegisterRoleViewModel
    {
        public SelectList Roles { get; set; }
        public SelectList Users { get; set; }
    }
}
