using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinkuSueldosYCompensacionesBackEnd.Context;
using RinkuSueldosYCompensacionesBackEnd.DAO;
using RinkuSueldosYCompensacionesBackEnd.Interfaces;
using RinkuSueldosYCompensacionesBackEnd.Models;
using RinkuSueldosYCompensacionesBackEnd.Models.Helpers;

namespace RinkuSueldosYCompensacionesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ISueldoMensualDTO _sueldoMensualDTO;
        private EmpleadoDAO _empleadoDao;

        public EmpleadosController(DataContext context,ISueldoMensualDTO sueldoMensual)
        {
            _context = context;
            _sueldoMensualDTO = sueldoMensual;
            _empleadoDao = new(_context.Database.GetConnectionString()!);
        }

        // GET: api/Empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GettblEmpleados()
        {
            try
            {
                IEnumerable<Empleado> empleados = await _empleadoDao.GetAll();
                return empleados.ToList();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            try
            {
                Empleado? empleado = await _empleadoDao.FindById(id);
                if (empleado == null) return NotFound();

                return empleado;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT: api/Empleados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
        {

            try
            {
                Empleado? empleadoExist = await _empleadoDao.FindById(id);
                if (empleadoExist == null) return NotFound();

                _empleadoDao.Update(empleado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Empleado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEmpleado(Empleado empleado)
        {
            try
            { 
                await _empleadoDao.Create(empleado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            try
            {
                await _empleadoDao.Delete(id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return NoContent();

        }

        [HttpPost("SueldoMensual")]
        public async Task<ActionResult<IEnumerable<SueldoMensual>>> PostEmpleadoSueldoMensual(FiltroSueldos filtro)
        {
            try
            {
                IEnumerable<SueldoMensual> sueldos = await _sueldoMensualDTO.GetSueldosMensualesAsync(filtro);
                return sueldos.ToArray();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            
        }
    }
}
