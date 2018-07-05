using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CCSmvc.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("ConnectionStringName")
        {
        }
        public DbSet<Payments> Payments { get; set; }

        public DbSet<Empldetails> EmpDetails { get; set; }

    }
}