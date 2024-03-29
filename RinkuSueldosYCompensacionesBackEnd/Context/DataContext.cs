﻿
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
        public DbSet<Movimiento> tblMovimientos { get; set; }
        public DbSet<EmpleadoRol> tblEmpleadosRoles { get; set; }
        public DbSet<CatMes> tblCatMeses { get; set; }
        public DbSet<PorcentajeVale> tblPorcentajeVales { get; set; }
        public DbSet<Retencion> tblRetenciones { get; set; }
       
    }
}
