using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TietoAngularAPI.Models;
using TietoAngularAPI.ViewModels;

namespace TietoAngularAPI.Controllers
{
    public class TuntiController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Tunti
        public ActionResult Index()
        {
            List<SimplyTunnitData> model = new List<SimplyTunnitData>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<Tunnit> tunnit = entities.Tunnit.OrderByDescending(Tunnit => Tunnit.Pvm).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (Tunnit tunti in tunnit)
                {
                    SimplyTunnitData tun = new SimplyTunnitData();
                    tun.Tunti_id = tunti.Tunti_id;
                    tun.Pvm = tunti.Pvm.GetValueOrDefault();
                    tun.ProjektiTunnit = tunti.ProjektiTunnit;

                    tun.Henkilo_id = tunti.Henkilot.Henkilo_id;
                    tun.Etunimi = tunti.Henkilot.Etunimi;
                    tun.Sukunimi = tunti.Henkilot.Sukunimi;

                    tun.Projekti_id = tunti.Projektit.Projekti_id;
                    tun.ProjektiNimi = tunti.Projektit.ProjektiNimi;
                    tun.Esimies = tunti.Projektit.Esimies;
                    tun.Avattu = tunti.Projektit.Avattu;
                    tun.Suljettu = tunti.Projektit.Suljettu;
                    tun.Status = tunti.Projektit.Status;

                    model.Add(tun);
                }

                return View(model);
            }

            finally
            {
                entities.Dispose();
            }
        }

        // GET: Tunti/Details/5
        public ActionResult Details(int? id)
        {
            List<SimplyTunnitData> model = new List<SimplyTunnitData>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                Tunnit tunti = entities.Tunnit.Find(id);
                if (tunti == null)
                {
                    return HttpNotFound();
                }

                Tunnit tuntidetail = entities.Tunnit.Find(tunti.Tunti_id);

                SimplyTunnitData tun = new SimplyTunnitData();
                tun.Tunti_id = tuntidetail.Tunti_id;
                tun.Pvm = tuntidetail.Pvm.GetValueOrDefault();
                tun.ProjektiTunnit = tuntidetail.ProjektiTunnit;

                tun.Henkilo_id = tuntidetail.Henkilot.Henkilo_id;
                tun.Etunimi = tuntidetail.Henkilot.Etunimi;
                tun.Sukunimi = tuntidetail.Henkilot.Sukunimi;

                tun.Projekti_id = tuntidetail.Projektit.Projekti_id;
                tun.ProjektiNimi = tuntidetail.Projektit.ProjektiNimi;
                tun.Esimies = tuntidetail.Projektit.Esimies;
                tun.Avattu = tuntidetail.Projektit.Avattu;
                tun.Suljettu = tuntidetail.Projektit.Suljettu;
                tun.Status = tuntidetail.Projektit.Status;

                ViewBag.ProjektiNimi = new SelectList((from pn in db.Projektit select new { Projekti_id = pn.Projekti_id, ProjektiNimi = pn.ProjektiNimi }), "Projekti_id", "ProjektiNimi", tun.Projekti_id);
                ViewBag.KokonimiH = new SelectList((from kn in db.Henkilot select new { Henkilo_id = kn.Henkilo_id, KokonimiH = kn.Etunimi + " " + kn.Sukunimi }), "Henkilo_id", "KokonimiH", tun.Henkilo_id);


            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }

        public ActionResult GetTunnit(int? id)
        {
            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            List<Tunnit> tunnit = (from t in entities.Tunnit
                                   where t.Projekti_id == id
                                   select t).ToList();

            List<SimplyTunnitData> result = new List<SimplyTunnitData>();

            CultureInfo fiFi = new CultureInfo("fi-FI");

            foreach (Tunnit tunti in tunnit)
            {
                SimplyTunnitData data = new SimplyTunnitData();

                data.Henkilo_id = (int)(tunti.Henkilo_id);
                data.Projekti_id = (int)tunti.Projekti_id;
                data.Tunti_id = tunti.Tunti_id;
                //data.Pvm = tunti.Pvm.Value.ToString(fiFi);
                data.Pvm = tunti.Pvm.Value;
                data.ProjektiTunnit = tunti.ProjektiTunnit;

                List<Henkilot> henkilot = (from p in entities.Henkilot
                                           where p.Henkilo_id == tunti.Henkilo_id
                                           select p).ToList();

                data.Etunimi = henkilot[0].Etunimi;
                data.Sukunimi = henkilot[0].Sukunimi;

                List<Projektit> projektit = (from p in entities.Projektit
                                             where p.Projekti_id == tunti.Projekti_id
                                             select p).ToList();
                data.ProjektiNimi = projektit[0].ProjektiNimi;
                data.Status = projektit[0].Status;
                data.Avattu = projektit[0].Avattu.Value;
                data.Suljettu = projektit[0].Suljettu.Value;

                result.Add(data);
            }
            entities.Dispose();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateTunnit()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            SimplyTunnitData model = new SimplyTunnitData();

            ViewBag.ProjektiNimi = new SelectList((from pn in db.Projektit select new { Projekti_id = pn.Projekti_id, ProjektiNimi = pn.ProjektiNimi }), "Projekti_id", "ProjektiNimi", null);
            ViewBag.KokonimiH = new SelectList((from kn in db.Henkilot select new { Henkilo_id = kn.Henkilo_id, KokonimiH = kn.Etunimi + " " + kn.Sukunimi }), "Henkilo_id", "KokonimiH", null);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTunnit(SimplyTunnitData model)
        {
            Tunnit tunnit = new Tunnit();
            tunnit.Tunti_id = model.Tunti_id;
            tunnit.Projekti_id = model.Projekti_id;
            tunnit.Henkilo_id = model.Henkilo_id;
            tunnit.Pvm = DateTime.Now;
            tunnit.ProjektiTunnit = model.ProjektiTunnit;

            db.Tunnit.Add(tunnit);

            int henkiloId = int.Parse(model.KokonimiH2);
            if (henkiloId > 0)
            {
                Tunnit tun = db.Tunnit.Find(henkiloId);
                tunnit.Henkilo_id = tun.Henkilo_id;
                ViewBag.KokonimiH = new SelectList((from kn in db.Henkilot select new { Henkilo_id = kn.Henkilo_id, KokonimiH = kn.Etunimi + " " + kn.Sukunimi }), "Henkilo_id", "KokonimiH", tunnit.Henkilo_id);

            }

            int projektiId = int.Parse(model.ProjektiNimi);
            if (projektiId > 0)
            {
                Projektit pro = db.Projektit.Find(projektiId);
                tunnit.Projekti_id = pro.Projekti_id;

            }

            ViewBag.ProjektiNimi = new SelectList((from pn in db.Projektit select new { Projekti_id = pn.Projekti_id, ProjektiNimi = pn.ProjektiNimi }), "Projekti_id", "ProjektiNimi", tunnit.Projekti_id);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }

        // GET: Tunti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tunnit tunti = db.Tunnit.Find(id);
            if (tunti == null)
            {
                return HttpNotFound();
            }

            SimplyTunnitData tun = new SimplyTunnitData();
            tun.Tunti_id = tunti.Tunti_id;
            tun.Pvm = tunti.Pvm;
            tun.ProjektiTunnit = tunti.ProjektiTunnit;

            tun.Henkilo_id = tunti.Henkilot.Henkilo_id;
            tun.Etunimi = tunti.Henkilot.Etunimi;
            tun.Sukunimi = tunti.Henkilot.Sukunimi;

            ViewBag.KokonimiH = new SelectList((from h in db.Henkilot select new { Henkilo_id = h.Henkilo_id, KokonimiH = h.Etunimi + " " + h.Sukunimi }), "Henkilo_id", "KokonimiH", tun.Henkilo_id);

            tun.Projekti_id = tunti.Projektit.Projekti_id;
            tun.ProjektiNimi = tunti.Projektit.ProjektiNimi;
            tun.Esimies = tunti.Projektit.Esimies;
            tun.Avattu = tunti.Projektit.Avattu;
            tun.Suljettu = tunti.Projektit.Suljettu;

            ViewBag.ProjektiNimi = new SelectList((from pn in db.Projektit select new { Projekti_id = pn.Projekti_id, ProjektiNimi = pn.ProjektiNimi }), "Projekti_id", "ProjektiNimi", tun.Projekti_id);


            return View(tun);
        }

        // POST: Tunti/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SimplyTunnitData model)
        {
            Tunnit tunnit = db.Tunnit.Find(model.Tunti_id);
            tunnit.Tunti_id = model.Tunti_id;
            //tunnit.Projekti_id = model.Projekti_id;
            //tunnit.Henkilo_id = model.Henkilo_id;
            tunnit.Pvm = model.Pvm;
            tunnit.ProjektiTunnit = model.ProjektiTunnit;

            int henkiloId = int.Parse(model.KokonimiH2);
            if (henkiloId > 0)
            {
                Henkilot hlo = db.Henkilot.Find(henkiloId);
                tunnit.Henkilo_id = hlo.Henkilo_id;
            }

            int projektiId = int.Parse(model.ProjektiNimi);
            if (projektiId > 0)
            {
                Projektit pro = db.Projektit.Find(projektiId);
                tunnit.Projekti_id = pro.Projekti_id;
            }

            ViewBag.ProjektiNimi = new SelectList((from pn in db.Projektit select new { Projekti_id = pn.Projekti_id, ProjektiNimi = pn.ProjektiNimi }), "Projekti_id", "ProjektiNimi", tunnit.Projekti_id);
            ViewBag.KokonimiH2 = new SelectList((from h in db.Henkilot select new { Henkilo_id = h.Henkilo_id, KokonimiH2 = h.Etunimi + " " + h.Sukunimi }), "Henkilo_id", "KokonimiH2", tunnit.Henkilo_id);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Tunti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tunnit tunnit = db.Tunnit.Find(id);
            if (tunnit == null)
            {
                return HttpNotFound();
            }
            return View(tunnit);
        }

        // POST: Tunti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tunnit tunnit = db.Tunnit.Find(id);
            db.Tunnit.Remove(tunnit);
            db.SaveChanges();

            ViewBag.ProjektiNimi = new SelectList((from pn in db.Projektit select new { Projekti_id = pn.Projekti_id, ProjektiNimi = pn.ProjektiNimi }), "Projekti_id", "ProjektiNimi", null);
            ViewBag.KokonimiH = new SelectList((from kn in db.Henkilot select new { Henkilo_id = kn.Henkilo_id, KokonimiH = kn.Etunimi + " " + kn.Sukunimi }), "Henkilo_id", "KokonimiH", null);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
