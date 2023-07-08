using Microsoft.EntityFrameworkCore;
using PreMatriculasParanoa.Domain.Models.Entities;

namespace PreMatriculasParanoa.Infra.Context
{
    public partial class EntityContext : DbContext
    {
        public EntityContext()
        {
        }

        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DbSgpmParanoa;Trusted_Connection=True;");
            }
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<PlanejamentoAnoLetivo> PlanejamentosAnosLetivos { get; set; }
        public DbSet<PlanejamentoSerieAno> PlanejamentosSeriesAnos { get; set; }
        public DbSet<PlanejamentoTurma> PlanejamentosTurmas { get; set; }
        public DbSet<PlanejamentoMatriculaSequencial> PlanejamentosMatriculasSequenciais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sala>()
                .HasOne(e => e.Escola)
                .WithMany(e => e.Salas)
                .HasForeignKey(e => e.IdEscola)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanejamentoAnoLetivo>()
                .HasOne(e => e.Escola)
                .WithMany(e => e.PlanejamentosAnosLetivos)
                .HasForeignKey(e => e.IdEscola)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanejamentoSerieAno>()
                .HasOne(e => e.PlanejamentoAnoLetivo)
                .WithMany(e => e.SeriesAnos)
                .HasForeignKey(e => e.IdPlanejamentoAnoLetivo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanejamentoTurma>()
                .HasOne(e => e.Sala)
                .WithMany(e => e.Turmas)
                .HasForeignKey(e => e.IdSala)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanejamentoTurma>()
                .HasOne(e => e.PlanejamentoSerieAno)
                .WithMany(e => e.Turmas)
                .HasForeignKey(e => e.IdPlanejamentoSerieAno)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
