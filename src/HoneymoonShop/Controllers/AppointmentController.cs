using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoneymoonShop.Model.AppointmentModels;
using HoneymoonShop.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoneymoonShop.Controllers
{
    public class AppointmentController : Controller
    {
        DBContext context;

        public AppointmentController(DBContext _context)
        {
            context = _context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["CSS"] = "Appointment.css";
            return View();
        }

        public IActionResult Stap2()
        {
            ViewData["CSS"] = "Appointment_stap2.css";
            return View("Stap2", new Appointment() { AppointmentTime = DateTime.UtcNow});
        }
        [HttpGet]
        public IActionResult Stap3(Appointment appointment, string confirmEmail)
        {
            return View("Stap3", appointment);
        }
    }
}
