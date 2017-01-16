using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels
{
    public class DressCategory
    {
        public int DressId { get; set; }
        public virtual Dress Dress { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
