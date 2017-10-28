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
    public class HenkiloController : Controller
    {
        private JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

        // GET: Henkilo
        public ActionResult Index()
        {

            List<SimplyTunnitData> model = new List<SimplyTunnitData>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                List<Henkilot> henkilot = entities.Henkilot.OrderByDescending(Henkilot => Henkilot.Sukunimi).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (Henkilot henkilo in henkilot)
                {
                    SimplyTunnitData hlo = new SimplyTunnitData();
                    hlo.Henkilo_id = henkilo.Henkilo_id;
                    hlo.Etunimi = henkilo.Etunimi;
                    hlo.Sukunimi = henkilo.Sukunimi;
                    hlo.Osoite = henkilo.Osoite;
                    hlo.Esimies = henkilo.Esimies;
                    hlo.Postinumero = henkilo.Postinumero;

                    model.Add(hlo);
                }

                return View(model);
            }

            finally
            {
                entities.Dispose();
            }
        }


        //2.vaihtoehto ilman ViewModel luokkaa
        //List<Henkilot> model = new List<Henkilot>();
        //try
        //{
        //    JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
        //    model = entities.Henkilot.ToList();
        //    entities.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    ViewBag.ErrorMessage = ex.GetType() + ": " + ex.Message;
        //}
        //return View(model);

        public ActionResult GetTunnit(int? id)
        {
            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            List<Tunnit> tunnit = (from t in entities.Tunnit
                                   where t.Henkilo_id == id
                                   select t).ToList();

            List<SimplyTunnitData> result = new List<SimplyTunnitData>();

            CultureInfo fiFi = new CultureInfo("fi-FI");

            foreach (Tunnit tunti in tunnit)
            {
                SimplyTunnitData data = new SimplyTunnitData();

                data.Tunti_id = tunti.Tunti_id;
                data.Henkilo_id = (int)(tunti.Henkilo_id);
                //data.Pvm = tunti.Pvm.Value.ToString(fiFi);
                data.Pvm = tunti.Pvm;
                data.ProjektiTunnit = (int)tunti.ProjektiTunnit;

                List<Projektit> projektit = (from p in entities.Projektit
                                             where p.Projekti_id == tunti.Projekti_id
                                             select p).ToList();

                data.ProjektiNimi = projektit[0].ProjektiNimi;

                result.Add(data);
            }

            entities.Dispose();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Henkilo/Details/5
        public ActionResult Details(int? id)
        {

            List<SimplyTunnitData> model = new List<SimplyTunnitData>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                Henkilot henkilodetail = entities.Henkilot.Find(id);
                if (henkilodetail == null)
                {
                    return HttpNotFound();
                }

                SimplyTunnitData hlo = new SimplyTunnitData();
                hlo.Henkilo_id = henkilodetail.Henkilo_id;
                hlo.Etunimi = henkilodetail.Etunimi;
                hlo.Sukunimi = henkilodetail.Sukunimi;
                hlo.Osoite = henkilodetail.Osoite;
                hlo.Esimies = henkilodetail.Esimies;
                hlo.Postinumero = henkilodetail.Postinumero;

            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }

        public ActionResult CreatePerson()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            Henkilot model = new Henkilot();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePerson(Henkilot model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            Henkilot henkilot = new Henkilot();
            henkilot.Henkilo_id = model.Henkilo_id;
            henkilot.Etunimi = model.Etunimi;
            henkilot.Sukunimi = model.Sukunimi;
            henkilot.Osoite = model.Osoite;
            henkilot.Esimies = model.Esimies;
            henkilot.Postinumero = model.Postinumero;

            db.Henkilot.Add(henkilot);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }

        // GET: Henkilo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null)
            {
                return HttpNotFound();
            }
            return View(henkilot);
        }

        // POST: Henkilo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Etunimi,Sukunimi,Osoite,Esimies,Postinumero,Henkilo_id")] Henkilot henkilot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(henkilot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(henkilot);
        }

        // GET: Henkilo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null)
            {
                return HttpNotFound();
            }
            return View(henkilot);
        }

        // POST: Henkilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Henkilot henkilot = db.Henkilot.Find(id);
            db.Henkilot.Remove(henkilot);
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
