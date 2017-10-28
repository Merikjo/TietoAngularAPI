using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TietoAngularAPI.Models;

namespace TietoAngularAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        //NorthwindDemo -tietokannan koodit:
        //[HttpGet]
        //[Route("api/Values/OrderCount")]
        //public int OrderCount()
        //{
        //    NorthwindDemoEntities entities = new NorthwindDemoEntities();
        //    int orderCount = entities.Orders.Count();
        //    entities.Dispose();
        //    return orderCount;
        //}

        //[HttpGet]
        //[Route("api/Values/LastNOrders/{id:int}")]
        //public List<string> LastNOrders(int id)
        //{
        //    NorthwindDemoEntities entities = new NorthwindDemoEntities();
        //    int numberOfOrdersToReturn = id;
        //    List<Orders> lastOrders =
        //        (from o in entities.Orders orderby o.OrderDate descending select o).Take(numberOfOrdersToReturn).ToList();
        //    List<string> customerNames =
        //        lastOrders.Select(o => o.Customers.CompanyName).ToList();
        //    entities.Dispose();
        //    return customerNames;
        //}

        [HttpGet]
        [Route("api/Values/ProjectCount")]
        public int ProjectCount()
        {
            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            int projectCount = entities.Projektit.Count();
            entities.Dispose();
            return projectCount;
        }

        [HttpGet]
        [Route("api/Values/PersonCount")]
        public int PersonCount()
        {
            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            int personCount = entities.Henkilot.Count();
            entities.Dispose();
            return personCount;
        }

        //TietokantaProjektin projekti.status listaus
        [HttpGet]
        [Route("api/Values/ProjectStatus/{id:int}")]
        public List<string> ProjectStatus(int id)
        {
            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            int projectStatusToReturn = id;
            List<Projektit> allProjects =
                (from p in entities.Projektit orderby p.Status descending select p).Take(projectStatusToReturn).ToList();
            List<string> projektiNimet =
                allProjects.Select(p => p.ProjektiNimi).ToList();
            //List<string> projektiStatus =
            // allProjects.Select(n => n.Status).ToList();
            entities.Dispose();
            return projektiNimet;
        }

        [HttpGet]
        [Route("api/Values/Henkilomaara/{id:int}")]
        public List<string> Henkilomaara(int id)
        {
            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            int henkilotToReturn = id;
            List<Henkilot> allHenkilot =
                (from h in entities.Henkilot orderby h.Sukunimi descending select h).Take(henkilotToReturn).ToList();
            List<string> henkiloNimet =
                allHenkilot.Select(h => h.Sukunimi).ToList();
            //List<string> projektiStatus =
            // allProjects.Select(n => n.Status).ToList();
            entities.Dispose();
            return henkiloNimet;
        }

        //[HttpGet]
        //[Route("api/Values/AllNCustomers/{id:int}")]
        //public string AllNCustomers(int id)
        //{
        //    NorthwindDemoEntities entities = new NorthwindDemoEntities();
        //    int allCustomersToList = id;
        //   
        //    //List<Customers> allCustomers = (from c in entities.Customers select c).Take(allCustomersToList).ToList();
        //    entities.Dispose();
        //    return allCustomers;
        //}
    }
}
