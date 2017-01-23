using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels
{
    

    public class Property
    {
        public enum PropertyType { Style, Neckline, Silhouette }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public virtual ICollection<DressProperty> Dresses { get; set; }

        public PropertyType getTypes(int enumNr){
            if(enumNr == 0)
            {
                return PropertyType.Style;
            } else if (enumNr == 1)
            {
                return PropertyType.Neckline;
            }
            else if (enumNr == 2)
            {
                return PropertyType.Silhouette;
            }
            else
            {
                return PropertyType.Silhouette;
            }
        }
    }
}
