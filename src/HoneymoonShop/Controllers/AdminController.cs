using HoneymoonShop.Model.DressModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoneymoonShop.Model;
using System.Data.SqlClient;

namespace HoneyMoonDB.Controllers {

    public class AdminController : Controller {

        readonly DBContext _context;
        NewDress model = new NewDress();

        public AdminController(DBContext context) {
            this._context = context;
        }

        // GET: /<Index>/
        public IActionResult Index() {
            return View();
        }

        // GET: /<Overview>/
        public IActionResult Overview() {
            return View();
        }

        [HttpGet]
        public IActionResult AddDress() {
            if (!model.redirected) {
                List<Property> properties = _context.Properties.ToList();
                List<Brand> brands = _context.Brands.ToList();
                model = new NewDress();
                model.AddBrands(brands);
                model.AddColors();
                model.AddProperties(properties);
            }

            return View("Create", model);
        }

        public IActionResult Catch (NewDress model) {
            Boolean problems = false;
            if (model.dress.Name.Length < 2) {
                ViewData["Name"] = "Naam is te kort";
                problems = true;
            }
            if (model.dress.Description.Length < 2) {
                ViewData["Description"] = "Beschrijving is te kort";
                problems = true;
            }
            if (model.brand == "") {
                ViewData["Brand"] = "Er is geen merk gekozen";
                problems = true;
            }
            if (model.silhouettes == 0) {
                ViewData["Silhouettes"] = "Er is geen silhouet gekozen.";
                problems = true;
            }
            if (model.PictureURLS.Count() < 1) {
                ViewData["PictureURLS"] = "Er moet minimaal 1 url meegegeven worden.";
                problems = true;
            } else {
                foreach (String picture in model.PictureURLS) {
                    foreach (String pictureUrl in model.PictureURLS) {
                        if (picture == pictureUrl) {
                            ViewData["PictureURLS"] = "Er zijn url's die hetzelfde zijn als een andere opgegeven URL. Dit is niet teogestaan.";
                            problems = true;
                        }
                    }
                }
            }
            if (model.Necklines.Count() < 1) {
                ViewData["Necklines"] = "Er moet minimaal 1 necklijn meegegeven worden.";
                problems = true;
            }
            if (model.Styles.Count() < 1) {
                ViewData["Styles"] = "Er moet minimaal 1 stijl meegegeven worden.";
                problems = true;
            }
            if (model.Colors.Count() < 1) {
                ViewData["Colors"] = "Er is geen kleur gekozen.";
                problems = true;
            }
            if (model.dress.PriceIndication < 10) {
                ViewData["PriceIndication"] = "Deze prijs is niet goed.";
                problems = true;
            }
            if (problems) {
                model.redirected = true;
                return View("Create", model);
            } else {
                return Create(model);
            }
        }

        public IActionResult Create(NewDress model)
        {
            Dress newDress = model.dress;

            List<int> Styles = model.Necklines.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            List<int> Necklines = model.Styles.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            List<Color> Colors = model.Colors.Where(x => x.IsChecked).Select(x => x.color).ToList();

            try {
                _context.Dresses.Add(newDress);


                foreach (string url in model.PictureURLS) {
                    _context.Images.Add(new Image() { DressId = newDress.DressId, DressURL = url });
                }

                foreach (int property in Styles) {
                    _context.DressProperties.Add(new DressProperty() { DressId = newDress.DressId, PropertyId = property });
                }

                foreach (int property in Necklines) {
                    _context.DressProperties.Add(new DressProperty() { DressId = newDress.DressId, PropertyId = property });
                }

                _context.DressProperties.Add(new DressProperty() { DressId = newDress.DressId, PropertyId = model.silhouettes });

                newDress.Colors = LeesKleuren<Color>();
                newDress.BrandName = model.brand;

                _context.SaveChanges();
                _context.SaveChanges();
            }
            catch (SqlException e)
            {
                ViewData["Message"] = "Jurk kon niet worden toegevoegd:";
                ViewData["Error"] = e.InnerException.Message;
                return View("Result", null);
            }

            ViewData["Message"] = "Jurk toegevoegd!";
            return View("Result", newDress);
        }

        public T LeesKleuren<T>()
        {
            int result = 0;
            Type type = typeof(T);
            try
            {
                foreach (T val in Enum.GetValues(typeof(T)))
                {
                    int i = 0;
                    if (type.Equals(Color.Ivory))
                    {
                        i = 0;
                    }
                    else if (type.Equals(Color.IvoryColor))
                    {
                        i = 1;
                    }
                    else if (type.Equals(Color.Color))
                    {
                        i = 2;
                    }
                    string attrName = "Colors[" + i + "].color";
                    if (Request.Form[attrName].Count > 0)
                    {
                        result = Convert.ToInt32(result) | Convert.ToInt32(val);
                    }
                }
            } catch (Exception) {
                result = 0;
            }

            return (T)Enum.ToObject(type, result);
        }

    }

}