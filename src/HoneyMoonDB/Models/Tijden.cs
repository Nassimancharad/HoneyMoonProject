using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HoneyMoonDB.Models
{
    public class Tijden
    {
        [Key]
        public DateTime AfspraakTijd { get; set; }
    }
  
}
