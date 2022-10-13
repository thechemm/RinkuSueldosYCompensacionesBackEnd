
using Microsoft.EntityFrameworkCore;
using RinkuSueldosYCompensacionesBackEnd.DAO;
using RinkuSueldosYCompensacionesBackEnd.Models;

namespace RinkuSueldosYCompensacionesBackEnd.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Empleado> tblEmpleados { get; set; }
       
    }
}
