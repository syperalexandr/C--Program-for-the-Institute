using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Lab3.Models;
using System.Collections.ObjectModel;

namespace Lab3.Controllers
{
    public class MedicinesController : ApiController
    {
        PersonsContext db = new PersonsContext();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            List<string> jsons = new List<string>();
            for (int i = 0; i < db.Medicines.Count(); i++)
            {
                jsons.Add(JsonConvert.SerializeObject(db.Medicines.ToList().ElementAt(i)));
            }
            return jsons;
        }

        public IHttpActionResult Delete(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            if (medicine != null)
            {
                db.Medicines.Remove(medicine);
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

            if (jsonString.IndexOf("custom") != -1)
            {
                List<string> jsons = new List<string>();
                for (int i = 0; i < db.Medicines.Count(); i++)
                {
                    if (db.Medicines.ToList().ElementAt(i).Disease.Equals(jsonObj["custom"]))
                    {
                        jsons.Add(JsonConvert.SerializeObject(db.Medicines.ToList().ElementAt(i)));
                    }
                }
                return Ok(jsons);
            }
            else
            {
                Medicine medicine = new Medicine
                {
                    Full_Name = jsonObj["pFullName"],
                    Address = jsonObj["pAddress"],
                    Disease = jsonObj["pDisease"],
                    Form = jsonObj["pForm"],
                    Treatment = jsonObj["pTreatment"],
                    Title = jsonObj["pTitle"],
                    Dosage = jsonObj["pDosage"],
                    NumPack = jsonObj["pNumPack"],
                    PricePack = jsonObj["pPricePack"]
                };
                db.Medicines.Add(medicine);
                db.SaveChanges();
                return Ok(medicine);
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
            Medicine medicine = new Medicine
            {
                Id = int.Parse(jsonObj["id"]),
                Full_Name = jsonObj["pFullName"],
                Address = jsonObj["pAddress"],
                Disease = jsonObj["pDisease"],
                Form = jsonObj["pForm"],
                Treatment = jsonObj["pTreatment"],
                Title = jsonObj["pTitle"],
                Dosage = jsonObj["pDosage"],
                NumPack = jsonObj["pNumPack"],
                PricePack = jsonObj["pPricePack"]
            };
            db.Entry(medicine).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(medicine);
        }
    }
}