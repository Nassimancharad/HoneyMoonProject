using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels
{
    /// <summary>
    /// Link-class between Dress and Property
    /// Foreign keys to Dress and Property
    /// </summary>
    public class DressProperty
    {
        public int DressId { get; set; }
        public virtual Dress Dress { get; set; }

        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}
