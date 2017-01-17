using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model
{
    public class BeschikbareTijden
    {
        [Key]
        public int ID { get; set; }
       
        public DateTime tijd { get; set; }
    }
}
