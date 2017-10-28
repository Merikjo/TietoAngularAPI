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
    public class ProjektiController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Projekti
        public ActionResult Index()
        {

            List<SimplyTunnitData> model = new List<SimplyTunnitData>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<Projektit> projektit = entities.Projektit.OrderByDescending(Projektit => Projektit.Avattu).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (Projektit projekti in projektit)
                {
                    SimplyTunnitData pro = new SimplyTunnitData();
                    pro.Projekti_id = projekti.Projekti_id;
                    pro.ProjektiNimi = projekti.ProjektiNimi;
                    pro.Esimies = projekti.Esimies;
                    pro.Avattu = projekti.Avattu;
                    pro.Suljettu = projekti.Suljettu;
                    pro.Status = projekti.Status;

                    model.Add(pro);
                }

                return View(model);
            }

            finally
            {
                entities.Dispose();
            }
        }

        public ActionResult GetProjektiTunnit(int? id)
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

                data.Henkilo_id = (int)tunti?.Henkilo_id;
                data.Projekti_id = (int)tunti.Projekti_id;
                data.Tunti_id = tunti.Tunti_id;

                List<Henkilot> henkilot = (from p in entities.Henkilot
                                           where p.Henkilo_id == tunti.Henkilo_id
                                           select p).ToList();

                data.Etunimi = henkilot[0].Etunimi;
                data.Sukunimi = henkilot[0].Sukunimi;

                //data.Pvm = tunti.Pvm.Value.ToString(fiFi);
                data.Pvm = tunti.Pvm;
                data.ProjektiTunnit = tunti.ProjektiTunnit;


                result.Add(data);
            }
            entities.Dispose();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Projekti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projektit projektit = db.Projektit.Find(id);
            if (projektit == null)
            {
                return HttpNotFound();
            }
            return View(projektit);
        }

        public ActionResult CreateProject()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            SimplyTunnitData model = new SimplyTunnitData();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject(SimplyTunnitData model)
        {
            Projektit pro = new Projektit();
            pro.Projekti_id = model.Projekti_id;
            pro.ProjektiNimi = model.ProjektiNimi;
            pro.Avattu = DateTime.Now;
            //pro.Suljettu = DateTime.Now;

            db.Projektit.Add(pro);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }

        // GET: Projekti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projektit projekti = db.Projektit.Find(id);
            if (projekti == null)
            {
                return HttpNotFound();
            }

            SimplyTunnitData pro = new SimplyTunnitData();
            pro.Projekti_id = projekti.Projekti_id;
            pro.ProjektiNimi = projekti.ProjektiNimi;
            pro.Esimies = projekti.Esimies;
            pro.Status = projekti.Status;
            //pro.Avattu = projekti.Avattu;
            //pro.Suljettu = projekti.Suljettu;

            ViewBag.ProjektiNimi = new SelectList((from pn in db.Projektit select new { Projekti_id = pn.Projekti_id, ProjektiNimi = pn.ProjektiNimi }), "Projekti_id", "ProjektiNimi", null);

            return View(pro);
        }

        // POST: Projekti/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SimplyTunnitData model)
        {

            Projektit pro = db.Projektit.Find(model.Projekti_id);
            pro.ProjektiNimi = model.ProjektiNimi;
            pro.Esimies = model.Esimies;
            pro.Status = model.Status;
            //pro.Avattu = DateTime.Now;
            //pro.Suljettu = DateTime.Now;

            db.SaveChanges();

            ViewBag.ProjektiNimi = new SelectList((from pn in db.Projektit select new { Projekti_id = pn.Projekti_id, ProjektiNimi = pn.ProjektiNimi }), "Projekti_id", "ProjektiNimi", null);

            return RedirectToAction("Index");
        }

        // GET: Projekti/ProAvattu/5
        public ActionResult ProAvattu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projektit projekti = db.Projektit.Find(id);
            if (projekti == null)
            {
                return HttpNotFound();
            }

            SimplyTunnitData pro = new SimplyTunnitData();
            pro.Projekti_id = projekti.Projekti_id;
            pro.ProjektiNimi = projekti.ProjektiNimi;
            pro.Esimies = projekti.Esimies;
            pro.Avattu = projekti.Avattu;
            //pro.Suljettu = projekti.Suljettu;

            return View(pro);
        }

        // POST: Projekti/ProAvattu/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProAvattu(SimplyTunnitData model)
        {

            Projektit pro = db.Projektit.Find(model.Projekti_id);
            pro.ProjektiNimi = model.ProjektiNimi;
            pro.Esimies = model.Esimies;
            pro.Avattu = DateTime.Now;
            //pro.Suljettu = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Projekti/ProSuljettu/5
        public ActionResult ProSuljettu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projektit projekti = db.Projektit.Find(id);
            if (projekti == null)
            {
                return HttpNotFound();
            }

            SimplyTunnitData pro = new SimplyTunnitData();
            pro.Projekti_id = projekti.Projekti_id;
            pro.ProjektiNimi = projekti.ProjektiNimi;
            pro.Esimies = projekti.Esimies;
            //pro.Avattu = projekti.Avattu;
            pro.Suljettu = projekti.Suljettu;

            return View(pro);
        }

        // POST: Projekti/ProSuljettu/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProSuljettu(SimplyTunnitData model)
        {

            Projektit pro = db.Projektit.Find(model.Projekti_id);
            pro.ProjektiNimi = model.ProjektiNimi;
            pro.Esimies = model.Esimies;
            //pro.Avattu = DateTime.Now;
            pro.Suljettu = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: Projekti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projektit projektit = db.Projektit.Find(id);
            if (projektit == null)
            {
                return HttpNotFound();
            }
            return View(projektit);
        }

        // POST: Projekti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projektit projektit = db.Projektit.Find(id);
            db.Projektit.Remove(projektit);
            db.SaveChanges();
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
