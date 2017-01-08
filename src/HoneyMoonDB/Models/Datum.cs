using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HoneyMoonDB.Models
{
    public class Datum
    {
        [Key]
        public DateTime datum { get; set; }
        [ForeignKey("Tijden")]
        public virtual ICollection<Tijden> AfspraakTijd { get; set; }

    }
}
