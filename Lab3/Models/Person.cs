using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class Person
    {
        public Person () { }

        public string Full_Name { get; set; }

        public string Address { get; set; }

        public int Id { get; set; }

        public Person(string Full_Name_Person, string Address_Person, int Id_Person)
        {
            Full_Name = Full_Name_Person;
            Address = Address_Person;
            Id = Id_Person;

        }

    }
}