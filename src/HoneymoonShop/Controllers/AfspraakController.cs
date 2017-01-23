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
using MimeKit;
using MailKit.Net.Smtp;
using System.Net;
using MailKit.Security;

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

      public IActionResult DatumSelecteren() {
    var tijden = HoneyMoonDb.BeschikbareTijden.ToList();

    //         Key       Value
    Dictionary<DateTime, bool[]> used = new Dictionary<DateTime, bool[]>();
    foreach (var afspraak in HoneyMoonDb.Afspraak) {
        if (!used.ContainsKey(afspraak.AfspraakDatum.Date)) {
            used[afspraak.AfspraakDatum.Date] = new bool[tijden.Count()];
        }

        //    Date                        Time (boolean)
        used[afspraak.AfspraakDatum.Date][tijden.IndexOf(afspraak.Tijd_FK)] = true;
    }

    List<string> list = new List<string>();
    foreach (var item in used) {
        if (tijden.Count() == item.Value.Where(a => a).Count())
            list.Add(item.Key.Date.ToString("dd-MM-yyyy"));
    }

    AfspraakVM vm = new AfspraakVM();
    vm.UitgeschakeldeDatum = list;

    // Geef de datum terug die geen tijden hebben.
    return View(vm);
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
        public async Task<IActionResult> GegevensBevestigen(Afspraak model) {


            if (ModelState.IsValid) {

                HoneyMoonDb.Afspraak.Add(model);
                HoneyMoonDb.SaveChanges();
                var tijdfk = HttpContext.Session.GetString("TijdFK");
                var body = "<p>Email van: {0} ({1})</p><p>Trouwdatum: {2}</p><p>TelNr: {3}</p><p>Uw afspraak is op {4} om {5}";
                var message = new MimeMessage();
                message.To.Add(new MailboxAddress(model.Naam, model.Email));
                message.From.Add(new MailboxAddress("hhshms4", "hhshms4@gmail.com"));
                message.Subject = "Honeymoon afspraak bevestiging";
                message.Body = new TextPart("html") { Text = string.Format(body, model.Naam, model.Email, model.TrouwDatum.ToString("dd/MM/yyyy"), model.Telefoonnummer, model.AfspraakDatum.ToString("dd/MM/yyyy"), tijdfk) };

                using (var smtp = new SmtpClient()) {
                    var credential = new NetworkCredential {
                        UserName = "hhshms4@gmail.com",  // replace with valid value
                        Password = "Asdfghjkl"  // replace with valid value
                    };
                    //smtp.LocalDomain = “domain.com”;

                    await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.Auto).ConfigureAwait(false);
                    await smtp.AuthenticateAsync(credential);
                    await smtp.SendAsync(message).ConfigureAwait(false);
                    await smtp.DisconnectAsync(true).ConfigureAwait(false);
                    return RedirectToAction("AfspraakVoltooid");
                }
            }
            return View(model);


        }

        public IActionResult AfspraakVoltooid()
        {
            return View();
        }
    }
}
