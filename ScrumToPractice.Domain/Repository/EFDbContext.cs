using ScrumToPractice.Domain.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<CorResposta> CorErrado { get; set; }
        public DbSet<CorSimulado> CorSimulado { get; set; }
        public DbSet<Cortesia> Cortesia { get; set; }
        public DbSet<Parametro> Parametro { get; set; }
        public DbSet<Questao> Questao { get; set; }
        public DbSet<Resposta> Resposta { get; set; }
        public DbSet<SimResposta> SimResposta { get; set; }
        public DbSet<SimQuestao> SimQuestao { get; set; }
        public DbSet<Simulado> Simulado { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }    
}
