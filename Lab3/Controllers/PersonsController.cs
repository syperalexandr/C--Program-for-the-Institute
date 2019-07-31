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
    public class PersonsController : ApiController
    {

        PersonsContext db = new PersonsContext();


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            List<string> jsons = new List<string>();
            for (int i = db.Persons.Count() - 1; i >= 0; i--)
            {
                jsons.Add(JsonConvert.SerializeObject(db.Persons.ToList().ElementAt(i)));
            }
            return jsons;
        }

        public Person Get(int id)
        {
            return db.Persons.Find(id);
        }


        public IHttpActionResult Delete(int id)
        {
            Person person = db.Persons.Find(id);
            if (person != null)
            {
                db.Persons.Remove(person);
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

            if (jsonString.IndexOf("disease_1") != -1)
            {
                var req = from s in db.Sicks.ToList()
                          .Where(s => s.Disease.Equals(jsonObj["disease_1"]) &&
                          s.Form.Equals("Легкая"))
                          select s;
                int result = req.Count();
                return Ok(result.ToString());
            }
            else if (jsonString.IndexOf("disease_2") != -1)
            {
                var req = from m in db.Medicines.ToList()
                          .Where(m => m.Disease.Equals(jsonObj["disease_2"]) &&
                          m.Form.Equals("Тяжелая") && m.Treatment.Equals("Больница"))
                          select m;
                List<Medicine> list = req.ToList();
                int result = 0;
                foreach (Medicine m in list)
                {
                    result += int.Parse(m.PricePack) * int.Parse(m.NumPack);
                }
                return Ok(result.ToString());
            }
            else
            {
                Person person = new Person
                {
                    Full_Name = jsonObj["pFullName"],
                    Address = jsonObj["pAddress"]

                };
                db.Persons.Add(person);
                db.SaveChanges();
                return Ok(person);
            }



        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(HttpRequestMessage request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var jsonString = request.Content.ReadAsStringAsync().Result;
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);           
            Person person = db.Persons.Find(int.Parse(jsonObj["id"]));
            person.Full_Name = jsonObj["pFullName"];
            person.Address = jsonObj["pAddress"];
            db.SaveChanges();
            return Ok(person);         
        }


    }
}