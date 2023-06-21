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

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DbSgpmParanoa;Trusted_Connection=True;");
            }
        }
    }
}
