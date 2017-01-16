using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoneymoonShop.Model;
using HoneymoonShop.Model.DressModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoneymoonShop.Controllers
{
    public class DressController : Controller
    {
        DBContext _context;

        public DressController(DBContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(int dressid)
        {
            ViewData["CSS"] = "selectedDress.css";

            Dress foundDress = _context.Dresses.Include("Brand").Include("Images").Include("Properties.Property").Include("Jewelry.Jewelry").
                FirstOrDefault(c => c.DressId == dressid);

            // Retrieve dress from DB
            

            if (foundDress == null)
            {
                ViewData["Title"]  = "Sorry, dressID is not found. Please try again!";

                return View("Error");
            }
            else
            {
                ViewData["Similar"] = GetSimilarDresses(foundDress, 4);
                return View(foundDress);
            }
        }
        
        public ICollection<Dress> GetSimilarDresses(Dress headDress, int amount)
        {
            List<Dress> SimilarList = new List<Dress>();

            foreach(DressProperty Property in headDress.Properties)
            {
                var dresses = _context.Dresses.Include("Images").Include("Properties.Property").Where(x => x.Properties.Any(y => y.Property == Property.Property));
                foreach (Dress Dress in dresses)
                {
                    if(!SimilarList.Contains(Dress) && SimilarList.Count < 4)
                    {
                        SimilarList.Add(Dress);
                    }
                }
            }
            int count = SimilarList.Count();
            int rest;
            if(amount-count > 0)
            {
                rest = amount - count;
            } else
            {
                rest = 0;
            }
            SimilarList.AddRange(_context.Dresses.Include("Images").Take(rest));
            return SimilarList;
        }    
    }
}
