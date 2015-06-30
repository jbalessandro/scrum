using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Repository
{
    public class EFDbContext: DbContext        
    {

        public EFDbContext()
            : base("ScrumEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // DbSets
        public DbSet<Area> Area { get; set; }
        public DbSet<CorErrado> CorErrado { get; set; }
        public DbSet<CorSimulado> CorSimulado { get; set; }
        public DbSet<Cortesia> Cortesia { get; set; }
        public DbSet<Parametro> Parametro { get; set; }
        public DbSet<Questao> Questao { get; set; }
        public DbSet<Resposta> Resposta { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }    
}
