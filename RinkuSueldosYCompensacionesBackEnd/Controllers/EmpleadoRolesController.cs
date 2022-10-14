using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinkuSueldosYCompensacionesBackEnd.Context;
using RinkuSueldosYCompensacionesBackEnd.Models;

namespace RinkuSueldosYCompensacionesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoRolesController : ControllerBase
    {
        private readonly DataContext _context;

        public EmpleadoRolesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/EmpleadoRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoRol>>> GettblEmpleadosRoles()
        {
            return await _context.tblEmpleadosRoles.ToListAsync();
        }

        // GET: api/EmpleadoRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoRol>> GetEmpleadoRol(int id)
        {
            var empleadoRol = await _context.tblEmpleadosRoles.FindAsync(id);

            if (empleadoRol == null)
            {
                return NotFound();
            }

            return empleadoRol;
        }

        // PUT: api/EmpleadoRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleadoRol(int id, EmpleadoRol empleadoRol)
        {
            if (id != empleadoRol.id)
            {
                return BadRequest();
            }

            _context.Entry(empleadoRol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoRolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmpleadoRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpleadoRol>> PostEmpleadoRol(EmpleadoRol empleadoRol)
        {
            _context.tblEmpleadosRoles.Add(empleadoRol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpleadoRol", new { id = empleadoRol.id }, empleadoRol);
        }

        // DELETE: api/EmpleadoRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleadoRol(int id)
        {
            var empleadoRol = await _context.tblEmpleadosRoles.FindAsync(id);
            if (empleadoRol == null)
            {
                return NotFound();
            }

            _context.tblEmpleadosRoles.Remove(empleadoRol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpleadoRolExists(int id)
        {
            return _context.tblEmpleadosRoles.Any(e => e.id == id);
        }
    }
}
