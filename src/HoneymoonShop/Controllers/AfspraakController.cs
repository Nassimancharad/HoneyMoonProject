using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HoneyMoonDB.Models;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Reflection;
using HoneymoonShop.Model;

namespace HoneyMoonDB.Controllers
{

    public class AfspraakController : Controller
    {

        private readonly DBContext HoneyMoonDb;

        public AfspraakController(DBContext context)
        {
            HoneyMoonDb = context;
        }

        // GET: Afspraak
        public async Task<IActionResult> Index()
        {
            return View(await HoneyMoonDb.Afspraak.ToListAsync());
        }

        // GET: Afspraak/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraak = await HoneyMoonDb.Afspraak.SingleOrDefaultAsync(m => m.AfspraakId == id);
            if (afspraak == null)
            {
                return NotFound();
            }

            return View(afspraak);
        }

        // GET: Afspraak/GegevensInvullenS
        public IActionResult GegevensInvullen()
        {

            return View();
        }

        // POST: Afspraak/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GegevensInvullen([Bind("AfspraakId,Email,Naam,Telefoonnummer,TrouwDatum,HerhaalEmail, AfspraakDatum, Tijd, Nieuwsbrief")] Afspraak afspraak, BeschikbareTijden tijden)
        {

            if (ModelState.IsValid)
            {
                //HoneyMoonDb.Add(afspraak);
                //HoneyMoonDb.SaveChanges();

                HttpContext.Session.SetString("Naam", afspraak.Naam);
                HttpContext.Session.SetString("TrouwDatum", afspraak.TrouwDatum.ToString());
                HttpContext.Session.SetInt32("TelNr", afspraak.Telefoonnummer);
                HttpContext.Session.SetString("Email", afspraak.Email);
                HttpContext.Session.SetString("HerhaalEmail", afspraak.HerhaalEmail);
                HttpContext.Session.SetString("AfspraakDatum", afspraak.AfspraakDatum.ToString());
                HttpContext.Session.SetString("Nieuwsbrief", afspraak.Nieuwsbrief.ToString());
                HttpContext.Session.SetInt32("Tijd", afspraak.Tijd);
                return RedirectToAction("GegevensBevestigen");
            }

            return View(afspraak);
        }

        // GET: Afspraak/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var afspraak = await HoneyMoonDb.Afspraak.SingleOrDefaultAsync(m => m.AfspraakId == id);
            if (afspraak == null)
            {
                return NotFound();
            }
            return View(afspraak);

        }

        // POST: Afspraak/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AfspraakId,Email,Naam,Nieuwsbrief,Telefoonnummer,Tijd,TrouwDatum")] Afspraak afspraak)
        {
            if (id != afspraak.AfspraakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HoneyMoonDb.Update(afspraak);
                    await HoneyMoonDb.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfspraakExists(afspraak.AfspraakId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(afspraak);

        }

        // GET: Afspraak/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraak = await HoneyMoonDb.Afspraak.SingleOrDefaultAsync(m => m.AfspraakId == id);
            if (afspraak == null)
            {
                return NotFound();
            }

            return View(afspraak);
        }

        // POST: Afspraak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var afspraak = await HoneyMoonDb.Afspraak.SingleOrDefaultAsync(m => m.AfspraakId == id);
            HoneyMoonDb.Afspraak.Remove(afspraak);
            await HoneyMoonDb.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public bool AfspraakExists(int id)
        {
            return HoneyMoonDb.Afspraak.Any(e => e.AfspraakId == id);
        }

        public IActionResult AfspraakMaken()
        {
            return View();
        }

        public IActionResult DatumSelecteren()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TijdSelecteren(string datGeselecteerdeDatum)
        {

            DateTime dt = datGeselecteerdeDatum != null ? DateTime.Parse(datGeselecteerdeDatum).Date : DateTime.Now.Date;
            var tijden = HoneyMoonDb.BeschikbareTijden.ToList();
            foreach (var afspraak in HoneyMoonDb.Afspraak)
            {
                if (afspraak.AfspraakDatum.Date.CompareTo(dt) == 0)
                {
                    tijden.Remove(afspraak.Tijd_FK);
                }
            }

            AfspraakVM vm = new AfspraakVM();
            vm.beschikbareTijden = tijden;

            return View(vm);
        }

        [HttpGet]
        public IActionResult GegevensBevestigen()
        {


            Afspraak afspraak = new Afspraak()
            {
                Naam = HttpContext.Session.GetString("Naam"),
                TrouwDatum = DateTime.Parse(HttpContext.Session.GetString("TrouwDatum")),
                Telefoonnummer = (int)HttpContext.Session.GetInt32("TelNr"),
                Email = HttpContext.Session.GetString("Email"),
                HerhaalEmail = HttpContext.Session.GetString("HerhaalEmail"),
                AfspraakDatum = DateTime.Parse(HttpContext.Session.GetString("AfspraakDatum")),
                Tijd = (int)HttpContext.Session.GetInt32("Tijd"),
                Tijd_FK = HoneyMoonDb.BeschikbareTijden.Where(a => a.ID == (int)HttpContext.Session.GetInt32("Tijd")).First(),
                Nieuwsbrief = Boolean.Parse(HttpContext.Session.GetString("Nieuwsbrief"))
            };

            return View(afspraak);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GegevensBevestigen(Afspraak a)
        {

            if (ModelState.IsValid)
            {
                HoneyMoonDb.Afspraak.Add(a);
                HoneyMoonDb.SaveChanges();

                return RedirectToAction("AfspraakVoltooid");
            }
         


            return View(a);
        }

        public IActionResult AfspraakVoltooid()
        {
            return View();
        }
    }
}
