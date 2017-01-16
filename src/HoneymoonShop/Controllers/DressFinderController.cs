using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoneymoonShop.Model.DressModels;
using HoneymoonShop.Model;
using Microsoft.EntityFrameworkCore;
using HoneymoonShop.Model.DressModels.FilterMenu;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoneymoonShop.Controllers
{
    public class DressFinderController : Controller
    {
        public delegate IQueryable<Dress> SortDresses(IQueryable<Dress> dresses);
        public class Sorter
        {
            static int _autoId = 0;
            public Sorter()
            {
                _autoId++;
                this.Id = _autoId;
            }
            public string Name { get; set; }
            public int Id { get; set; }
            public SortDresses Sort { get; set; }
        }
        static readonly List<Sorter> Sorters = new List<Sorter>()
        {
            new Sorter() {Name="Prijs Laag/Hoog", Sort = x => x.OrderBy(d=>d.PriceIndication)},
            new Sorter() {Name="Prijs Hoog/Laag", Sort = x => x.OrderByDescending(d=>d.PriceIndication)},
        };

        List<Sorter> sorters = Sorters;
        DBContext _context;

        public DressFinderController(DBContext context)
        {
            _context = context;
        }

        public void SetSorters(List<Sorter> sorters)
        {
            this.sorters = sorters;
        }

        public IActionResult Index()
        {

            ViewData["CSS"] = "site.css";
            var dresses = _context.Dresses.Include("Images").Take(6).ToList();
            var model = GetFilterData(null);
            model.Dresses = dresses;
            return View(model);
        }

        private bool isItemInitialized(SearchItems items)
        {
            return (items?.Initialized ?? false); 
        }

        public class BrowseParameters
        {
            public int Page { get; set; } = 1;
            public int ItemCount { get; set; } = 24;
            public int Category { get; set; } = -1;
            public int? Sort { get; set; } = null;

            public override string ToString()
            {
                return this.GetQueryString();
            }
        }

        // GET: /<controller>/
        // dressfinder.com/dressfinder/?page=5&items=24
        public IActionResult Browse(SearchItems item = null, BrowseParameters parameters = null)
        {
            if (!isItemInitialized(item))
                item = null;
            ViewData["CSS"] = "site.css";
            if(parameters == null)
                parameters = new BrowseParameters();
            var dresses = getDressessWithImagesAndCategories();
            var categoryDresses = getDressesBelongingToCategory(parameters.Category, dresses) ?? dresses;
            var filteredDresses = item != null
                ? FilterDress(item, categoryDresses)
                : categoryDresses;

            int pages = getTotalNumberOfPages(filteredDresses, parameters.ItemCount);
            
            if (parameters.Page > pages)
                parameters.Page = pages;

            Sorter sorter = null;

            var sortedAndFilteredCategoryDresses = sortDresses(filteredDresses, parameters.Sort, ref sorter) ?? filteredDresses;

            setViewDataVariables(sorters, ref sorter, pages);
            var dressesToDisplay = getDressesToDisplay(sortedAndFilteredCategoryDresses, parameters.Page, parameters.ItemCount);

            SuperModel m = GetFilterData(parameters.Category);
            m.Parameters = parameters;
            if (item != null) m.ApplySelectedFilter(item);
            m.Dresses = dressesToDisplay;

            return View("Browse", m);
        }

        public SuperModel GetFilterData(int? category = -1)
        {
            List<Property> properties = _context.Properties.ToList();
            List<Brand> brands = _context.Brands.ToList();
            SuperModel model = new SuperModel(properties, brands)
            {
                MinPrice = _context.Dresses.Min(dress => dress.PriceIndication),
                MaxPrice = _context.Dresses.Max(dress => dress.PriceIndication)
            };
            model.SelectedMinPrice = model.MinPrice;
            model.SelectedMaxPrice = model.MaxPrice;
            // category
            model.Categories = _context.Categories.ToList();
            model.CurrentCategory = category;
            return model;
        }

        public bool GetPriceRange(string range, out int min, out int max)
        {
            string[] split = range.Split(',');
            try
            {
                min = int.Parse(split[0]);
                max = int.Parse(split[1]);
                return true;
            }
            catch (Exception)
            {
                min = 0;
                max = 100000000;
                return false;
            }
        }

        public IActionResult Add(SuperModel collectie, string priceRange, BrowseParameters parameters)
        {
            int minPrice, maxPrice;
            GetPriceRange(priceRange, out minPrice, out maxPrice);
            collectie.SelectedMinPrice = minPrice;
            collectie.SelectedMaxPrice = maxPrice;
            SearchItems item = new SearchItems(collectie);
            string url = "Browse?" + item.GetQueryString() + parameters.GetQueryString();
            return Redirect(url);
        }

        public IQueryable<Dress> FilterDress(SearchItems item, IQueryable<Dress> dresses)
        {
            if (item.Brands.Any())
                dresses = dresses.Where(dress => item.Brands.Contains(dress.BrandName));
            if (item.Necklines.Any())
            {
                dresses =
                    dresses.Where(
                        dress => dress.Properties.Any(dProp => item.Necklines.Contains(dProp.Property.PropertyId)));
            }
            if (item.Silhouettes.Any())
            {
                dresses =
                    dresses.Where(
                        dress => dress.Properties.Any(dProp => item.Silhouettes.Contains(dProp.Property.PropertyId)));
            }
            if (item.Styles.Any())
            {
                dresses =
                    dresses.Where(
                        dress => dress.Properties.Any(dProp => item.Styles.Contains(dProp.Property.PropertyId)));
            }
            if (item.Colors.Any())
            {
                dresses = dresses.Where(dress => item.Colors.Any(color => (dress.Colors & color) == color));
            }
            if(item.SelectedMaxPrice != null && item.SelectedMinPrice != null)
                dresses = dresses.Where(dress => dress.PriceIndication >= item.SelectedMinPrice && dress.PriceIndication <= item.SelectedMaxPrice);
            return dresses;
        }

        private IQueryable<Dress> getDressessWithImagesAndCategories() => _context.Dresses.Include("Images").Include("Categories");




        private bool isChosenCatExistent(int catId)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.CategoryId == catId);

            return category != null;

        }

        private IQueryable<Dress> getDressesBelongingToCategory(int catId, IQueryable<Dress> dresses)
        {
            if (isChosenCatExistent(catId))
            {
                return dresses.Where(d => d.Categories.Any(c => c.CategoryId == catId));
            }
            return null;
        }


        private int getTotalNumberOfPages(IQueryable<Dress> categoryDresses, int items)
        {
            int totalItems = categoryDresses.Count();
            return (int)Math.Ceiling((totalItems / (double)items));
        }

        private IQueryable<Dress> sortDresses(IQueryable<Dress> categoryDresses, int? sort, ref Sorter sorter)
        {
            if (sort != null)
            {
                sorter = sorters.FirstOrDefault(s => s.Id == sort);
                if (sorter != null)
                    return categoryDresses = sorter.Sort(categoryDresses);
            }
            return null;
        }

        private void setViewDataVariables(List<Sorter> sorters, ref Sorter sorter, int pages)
        {
            ViewData["Sorters"] = sorters;
            ViewData["Sorter"] = sorter;

            ViewData["PageCount"] = pages;
        }

        private List<Dress> getDressesToDisplay(IQueryable<Dress> sortedCategoryDresses, int page, int items)
        {

            if (!sortedCategoryDresses.Any())
                return sortedCategoryDresses.ToList(); 
            var dressesToDisplay = sortedCategoryDresses.
                       Skip((page - 1) * items).
                       Take(items).ToList();

            return dressesToDisplay;
        }
    }
}
