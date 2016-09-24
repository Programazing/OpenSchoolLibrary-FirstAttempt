using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Models.DatabaseModels
{
    public class Setting
    {
        public int SettingID { get; set; }
        [Column(TypeName = "Money")]
        public decimal? FineAmount { get; set; }
    }
}
