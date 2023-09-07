using Microsoft.EntityFrameworkCore;
using sifreKasa.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sifreKasa.Data.Context
{
    public class sifreKasaDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=SifreKasaDb;Encrypt=True;TrustServerCertificate=True;Integrated Security=True;");
            }
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Accounts> Accounts { get; set; } 

    }
}

