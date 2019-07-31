using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class SicksController : ApiController
    {
        PersonsContext db = new PersonsContext();

        // GET api/<controller>
        // IEnumerable<string> - возращаемый тип  (Интерфейс)
        public IEnumerable<string> Get()
        {
            List<string> jsons = new List<string>();
            for (int i = 0; i < db.Sicks.Count(); i++)
            {
                jsons.Add(JsonConvert.SerializeObject(db.Sicks.ToList().ElementAt(i)));
            }
            return jsons;
        }

        public Sick Get(int id)
        {
            return db.Sicks.Find(id);
        }


        public IHttpActionResult Delete(int id)
        {
            Sick sick = db.Sicks.Find(id);
            if (sick != null)
            {
                db.Sicks.Remove(sick);
                db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }


        // POST api/<controller>
        public IHttpActionResult Post(HttpRequestMessage request)
        {
            var jsonString = request.Content.ReadAsStringAsync().Result;
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

            Sick sick = new Sick
            {
                Full_Name = jsonObj["pFullName"],
                Address = jsonObj["pAddress"],
                Disease = jsonObj["pDisease"],
                Form = jsonObj["pForm"],
                Treatment = jsonObj["pTreatment"]
            };
            db.Sicks.Add(sick);
            db.SaveChanges();
            return Ok(sick);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(HttpRequestMessage request)
        {
            var jsonString = request.Content.ReadAsStringAsync().Result;
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            Sick sick = new Sick
            {
                Id = int.Parse(jsonObj["id"]),
                Full_Name = jsonObj["pFullName"],
                Address = jsonObj["pAddress"],
                Disease = jsonObj["pDisease"],
                Form = jsonObj["pForm"],
                Treatment = jsonObj["pTreatment"]
            };
            db.Entry(sick).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(sick);
        }
    }
}