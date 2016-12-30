using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HoneyMoonDB.Data;
using HoneyMoonDB.Models;

namespace HoneyMoonDB.Controllers {

    public class AfspraakController : Controller {

        private readonly HoneyMoonDbContext HoneyMoonDb;

        public AfspraakController(HoneyMoonDbContext context) {
            HoneyMoonDb = context;    
        }

        // GET: Afspraak
        public async Task<IActionResult> Index() {
            return View(await HoneyMoonDb.Afspraak.ToListAsync());
        }

        // GET: Afspraak/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var afspraak = await HoneyMoonDb.Afspraak.SingleOrDefaultAsync(m => m.AfspraakId == id);
            if (afspraak == null) {
                return NotFound();
            }

            return View(afspraak);
        }

        // GET: Afspraak/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Afspraak/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GegevensInvullen([Bind("AfspraakId,Email,Naam,Nieuwsbrief,Telefoonnummer,Tijd,TrouwDatum,HerhaalEmail ")] Afspraak afspraak) {

            if (ModelState.IsValid) {
                //HoneyMoonDb.Add(afspraak);
                //HoneyMoonDb.SaveChanges();
                return RedirectToAction("GegevensBevestigen");
            }
            return View(afspraak);
        }

        // GET: Afspraak/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {
                return NotFound();
            }

            var afspraak = await HoneyMoonDb.Afspraak.SingleOrDefaultAsync(m => m.AfspraakId == id);
            if (afspraak == null) {
                return NotFound();
            }
            return View(afspraak);

        }

        // POST: Afspraak/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AfspraakId,Email,Naam,Nieuwsbrief,Telefoonnummer,Tijd,TrouwDatum")] Afspraak afspraak) {
            if (id != afspraak.AfspraakId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    HoneyMoonDb.Update(afspraak);
                    await HoneyMoonDb.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!AfspraakExists(afspraak.AfspraakId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(afspraak);

        }

        // GET: Afspraak/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var afspraak = await HoneyMoonDb.Afspraak.SingleOrDefaultAsync(m => m.AfspraakId == id);
            if (afspraak == null) {
                return NotFound();
            }

            return View(afspraak);
        }

        // POST: Afspraak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var afspraak = await HoneyMoonDb.Afspraak.SingleOrDefaultAsync(m => m.AfspraakId == id);
            HoneyMoonDb.Afspraak.Remove(afspraak);
            await HoneyMoonDb.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public bool AfspraakExists(int id) {
            return HoneyMoonDb.Afspraak.Any(e => e.AfspraakId == id);
        }

        public IActionResult AfspraakMaken() {
            return View();
        }

        public IActionResult DatumSelecteren() {
            return View();
        }

        public IActionResult TijdSelecteren() {
            return View();
        }

        public IActionResult GegevensInvullen() {
            return View();
        }

        public IActionResult GegevensBevestigen() {
            return View();
        }

        public IActionResult AfspraakVoltooid() {
            return View();
        }
    }
}
