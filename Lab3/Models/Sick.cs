using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class Sick : Person
    {
        public string Disease { get; set; }

        public string Form { get; set; }

        public string Treatment { get; set; }

        public Sick() { }

        public Sick(string Full_Name_Person, string Address_Person, string Disesase_Sick, string Form_Sick, string Treatment_Sick, int Id_Sick) : base(Full_Name_Person, Address_Person, Id_Sick)
        {
            Disease = Disesase_Sick;
            Form = Form_Sick;
            Treatment = Treatment_Sick;
        }
    }
}