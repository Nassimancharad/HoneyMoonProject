using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model
{
    public class BeschikbareTijden
    {
        
        public int tijdID { get; set; }
        [Key]
        public DateTime tijd { get; set; }
    }
}
