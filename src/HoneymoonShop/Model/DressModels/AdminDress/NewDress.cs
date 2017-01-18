using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoneymoonShop.Model.DressModels.FilterMenu;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoneymoonShop.Model.DressModels
{
    public class NewDress
    {
        public Dress dress { get; set; } = new Dress();
        public List<Brand> Brands { get; set; } = new List<Brand>();
        public List<CheckBoxItem> Styles { get; set; } = new List<CheckBoxItem>();
        public List<CheckBoxItem> Necklines { get; set; } = new List<CheckBoxItem>();
        public List<Property> Silhouettes { get; set; } = new List<Property>();
        public List<CheckBoxItemColor> Colors { get; set; } = new List<CheckBoxItemColor>();
        [Required]
       
        public List<String> PictureURLS { get; set; } = new List<String>();
        [Required]
        public String brand { get; set; }
        [Required]
        public int silhouettes { get; set; }
        public Boolean redirected { get; set; } = false;
         


        public void AddColors()
        {
            foreach (Color value in Enum.GetValues(typeof(Color)))
            {
                var attrib = value.GetAttributeOfType<DisplayAttribute>();
                var item = new CheckBoxItemColor()
                {
                    color = value,
                    Display = attrib.Name,
                    IsChecked = false
                };
                Colors.Add(item);
            }

        }

        public void AddProperties(List<Property> properties)
        {
            foreach (Property p in properties)
            {
                var item = new CheckBoxItem()
                {
                    ID = p.PropertyId,
                    Display = p.Name,
                    IsChecked = false
                };

                switch (p.Type)
                {
                    case Property.PropertyType.Style:
                        Styles.Add(item);
                        break;
                    case Property.PropertyType.Neckline:
                        Necklines.Add(item);
                        break;
                    case Property.PropertyType.Silhouette:
                        Silhouettes.Add(p);
                        break;
                }
            }
        }

        public void AddBrands(List<Brand> brands)
        {
            Brands = brands;
        }
    }
}
