using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Open_School_Library.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public string Email { get; set; }
        public IList<string> Roles { get; set; }

    }
}
