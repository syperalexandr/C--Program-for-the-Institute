using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class Medicine : Sick
    {
        public string Title { get; set; }

        public string Dosage { get; set; }

        public string NumPack { get; set; }

        public string PricePack { get; set; }

        public Medicine() { }

        public Medicine(string Full_Name_Person, string Address_Person, string Disesase_Sick, string Form_Sick, string Treatment_Sick, string Title_Medicine, string Dosage_Medicine, string Num_pack_Medicine, string Price_pack_Medicine, int Id_Medicine) : base(Full_Name_Person, Address_Person, Disesase_Sick, Form_Sick, Treatment_Sick, Id_Medicine)
        {
            Title = Title_Medicine;
            Dosage = Dosage_Medicine;
            NumPack = Num_pack_Medicine;
            PricePack = Price_pack_Medicine;
        }
    }
}