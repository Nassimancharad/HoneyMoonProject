using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HoneyMoonDB.Controllers
{
    public class WinkelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}