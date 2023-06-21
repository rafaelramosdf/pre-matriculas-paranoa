using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PreMatriculasParanoa.Infra.Context
{
    public partial class EntityContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public EntityContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
