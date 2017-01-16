using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels
{
    public class Jewelry
    {
        public int JewelryId { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public virtual ICollection<DressJewelry> Dresses { get; set; }
    }

}
