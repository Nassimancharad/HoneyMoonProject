using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels
{
    public class DressProperty
    {
        public int DressId { get; set; }
        public virtual Dress Dress { get; set; }

        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}
