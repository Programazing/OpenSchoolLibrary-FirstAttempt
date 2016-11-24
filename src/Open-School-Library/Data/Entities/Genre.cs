using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Open_School_Library.Data.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }

        public string Name { get; set; }
    }
}
