using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels
{
    public class DressJewelry
    {
        public int DressId { get; set; }
        public int JewelryId { get; set; }

        public virtual Dress Dress { get; set; }
        public virtual Jewelry Jewelry { get; set; }
    }
}
