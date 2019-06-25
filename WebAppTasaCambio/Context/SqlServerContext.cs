using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebAppTasaCambio.Models;

namespace WebAppTasaCambio.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext() :base ("MyConn")
        {

        }

        public DbSet<TasaCambioDiaria> TasaCambioDiaria { get; set; }
        public DbSet<BasesDatos> BasesDatos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<TasaCambioDiaria>()
                .Property(p => p.Compra)
                .HasPrecision(15, 4);
            modelBuilder.Entity<TasaCambioDiaria>()
                .Property(p => p.Venta)
                .HasPrecision(15, 4);
                

        }
    }
}