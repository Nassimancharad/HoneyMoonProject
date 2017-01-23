using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels
{
    public class Property {
        public enum PropertyType { Style, Neckline, Silhouette }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public virtual ICollection<DressProperty> Dresses { get; set; }

        public PropertyType getTypes(int type){
            if(type == 0) {
                return PropertyType.Style;
            } else if (type == 1) {
                return PropertyType.Neckline;
            }
            else if (type == 2) {
                return PropertyType.Silhouette;
            } else  {
                return PropertyType.Silhouette;
            }
        }
    }
}
