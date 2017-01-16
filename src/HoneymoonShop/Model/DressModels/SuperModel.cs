using HoneymoonShop.Model.DressModels.FilterMenu;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HoneymoonShop.Controllers;

namespace HoneymoonShop.Model.DressModels
{
    public class SuperModel
    {
        public List<Dress> Dresses { get; set; }
        public DressFinderController.BrowseParameters Parameters { get; set; }
        // filters
        public SearchItems SelectedFilter { get; set; } = null;
        public List<CheckBoxItemBrands> Brands { get; set; } = new List<CheckBoxItemBrands>();
        public List<CheckBoxItem> Styles { get; set; } = new List<CheckBoxItem>();
        public List<CheckBoxItem> Necklines { get; set; } = new List<CheckBoxItem>();
        public List<CheckBoxItem> Silhouettes { get; set; } = new List<CheckBoxItem>();
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int SelectedMinPrice { get; set; }
        public int SelectedMaxPrice { get; set; }
        public List<CheckBoxItemColor> Colors { get; set; } = new List<CheckBoxItemColor>();
        // category
        public List<Category> Categories { get; set; }
        public int? CurrentCategory { get; set; }

        public SuperModel() { }

        public SuperModel(List<Property> properties, List<Brand> brands )
        {
            AddColors();
            AddProperties(properties);
            AddBrands(brands);
        }

        void AddColors()
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

        void AddProperties(List<Property> properties)
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
                        Silhouettes.Add(item);
                        break;
                }
            }
        }

        void AddBrands(List<Brand> brands)
        {
            foreach (Brand brand in brands)
            {
                var item = new CheckBoxItemBrands()
                {
                    ID = brand.Name,
                    Display = brand.Name,
                    IsChecked = false
                };
                Brands.Add(item);
            }
        }

        public void ApplySelectedFilter(SearchItems filter)
        {
            this.SelectedFilter = filter;
            foreach (string brand in filter.Brands)
            {
                CheckBoxItemBrands item = Brands.FirstOrDefault(i => i.ID == brand);
                if (item != null)
                    item.IsChecked = true;
            }
            foreach (Color color in filter.Colors)
            {
                var item = Colors.FirstOrDefault(i => i.color == color);
                if (item != null)
                    item.IsChecked = true;
            }
            ApplyPropertyFilters(Styles, filter.Styles);
            ApplyPropertyFilters(Necklines, filter.Necklines);
            ApplyPropertyFilters(Silhouettes, filter.Silhouettes);
            SelectedMinPrice = filter.SelectedMinPrice ?? MinPrice;
            SelectedMaxPrice = filter.SelectedMaxPrice ?? MaxPrice;
        }

        void ApplyPropertyFilters(List<CheckBoxItem> modelList, List<int> filterList)
        {
            foreach (int propertyID in filterList)
            {
                var item = modelList.FirstOrDefault(i => i.ID == propertyID);
                if (item != null)
                    item.IsChecked = true;
            }
        }
    }
}
