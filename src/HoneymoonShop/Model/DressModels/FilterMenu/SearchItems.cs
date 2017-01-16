using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoneymoonShop.Model.DressModels.FilterMenu
{
    public class SearchItems
    {
        public bool Initialized { get; set; } = false;
        public List<int> Styles { get; set; } = new List<int>();
        public List<int> Necklines { get; set; } = new List<int>();
        public List<int> Silhouettes { get; set; } = new List<int>();
        public List<string> Brands { get; set; } = new List<string>();
        public int? SelectedMinPrice { get; set; } = null;
        public int? SelectedMaxPrice { get; set; } = null;
        public List<Color> Colors { get; set; } = new List<Color>();

        public SearchItems() { }

        public SearchItems(SuperModel model)
        {
            Styles = model.Necklines.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            Silhouettes = model.Silhouettes.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            Necklines = model.Styles.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            Brands = model.Brands.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            SelectedMinPrice = model.SelectedMinPrice;
            SelectedMaxPrice = model.SelectedMaxPrice;
            Colors = model.Colors.Where(x => x.IsChecked).Select(x => x.color).ToList();
            Initialized = true;
        }
    }
}
