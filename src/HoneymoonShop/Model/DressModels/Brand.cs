using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels
{
    public class Brand
    {
        [Key]
        public string Name { get; set; }
        public virtual ICollection<Dress> Dresses { get; set; }
        public virtual ICollection<Jewelry> Sieraden { get; set; }
    }
}
