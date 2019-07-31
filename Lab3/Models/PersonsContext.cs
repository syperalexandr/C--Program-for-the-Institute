using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Lab3.Models
{
    public class PersonsContext : DbContext

    {
        public PersonsContext(): base("DefaultConnection")
        {

        }
        // 3 необходимые сущности
        public DbSet<Person> Persons { get; set; }
        public DbSet<Sick> Sicks { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
    }
}