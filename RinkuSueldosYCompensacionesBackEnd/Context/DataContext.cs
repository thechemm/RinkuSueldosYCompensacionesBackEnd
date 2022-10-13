
using Microsoft.EntityFrameworkCore;

namespace aprendo_mas_backend.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Usuario> tblUsuarios { get; set; }
        public DbSet<UsuarioPerfil> tblUsarioPerfiles { get; set; }
        public DbSet<Tesis> tblTesis { get; set; }
        public DbSet<Carrera> tblCarreras { get; set; }
    }
}
