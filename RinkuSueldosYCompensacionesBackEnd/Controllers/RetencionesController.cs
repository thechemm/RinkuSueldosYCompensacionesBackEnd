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
    public class RetencionesController : ControllerBase
    {
        private readonly DataContext _context;

        public RetencionesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Retenciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Retencion>>> GettblRetenciones()
        {
            return await _context.tblRetenciones.ToListAsync();
        }

        // GET: api/Retenciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Retencion>> GetRetencion(int id)
        {
            var retencion = await _context.tblRetenciones.FindAsync(id);

            if (retencion == null)
            {
                return NotFound();
            }

            return retencion;
        }

        // PUT: api/Retenciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRetencion(int id, Retencion retencion)
        {
            if (id != retencion.id)
            {
                return BadRequest();
            }

            _context.Entry(retencion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RetencionExists(id))
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

        // POST: api/Retenciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Retencion>> PostRetencion(Retencion retencion)
        {
            _context.tblRetenciones.Add(retencion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRetencion", new { id = retencion.id }, retencion);
        }

        // DELETE: api/Retenciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRetencion(int id)
        {
            var retencion = await _context.tblRetenciones.FindAsync(id);
            if (retencion == null)
            {
                return NotFound();
            }

            _context.tblRetenciones.Remove(retencion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RetencionExists(int id)
        {
            return _context.tblRetenciones.Any(e => e.id == id);
        }
    }
}
