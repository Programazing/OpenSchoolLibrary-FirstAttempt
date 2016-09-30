using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.SettingViewModel
{
    public class SettingIndexViewModel
    {
        public int SettingID { get; set; }
        [Display(Name ="Fine Ammount Per Day")]

        public decimal? FineAmountPerDay { get; set; }
        [Display(Name = "Checkout Duration in Days")]
        public int CheckoutDurationInDays { get; set; }
    }
}
