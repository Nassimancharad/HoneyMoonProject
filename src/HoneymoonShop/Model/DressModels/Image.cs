using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels
{
    public class Image
    {
        [ForeignKey("Dress")]
        public int DressId { get; set; }
        public virtual Dress Dress { get; set; }
        public string DressURL { get; set; }
    }
}