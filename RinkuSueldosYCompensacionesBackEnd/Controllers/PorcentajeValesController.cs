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
    public class PorcentajeValesController : ControllerBase
    {
        private readonly DataContext _context;

        public PorcentajeValesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PorcentajeVales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PorcentajeVale>>> GettbltblPorcentajeVales()
        {
            return await _context.tbltblPorcentajeVales.ToListAsync();
        }

        // GET: api/PorcentajeVales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PorcentajeVale>> GetPorcentajeVale(int id)
        {
            var porcentajeVale = await _context.tbltblPorcentajeVales.FindAsync(id);

            if (porcentajeVale == null)
            {
                return NotFound();
            }

            return porcentajeVale;
        }

        // PUT: api/PorcentajeVales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPorcentajeVale(int id, PorcentajeVale porcentajeVale)
        {
            if (id != porcentajeVale.id)
            {
                return BadRequest();
            }

            _context.Entry(porcentajeVale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PorcentajeValeExists(id))
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

        // POST: api/PorcentajeVales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PorcentajeVale>> PostPorcentajeVale(PorcentajeVale porcentajeVale)
        {
            _context.tbltblPorcentajeVales.Add(porcentajeVale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPorcentajeVale", new { id = porcentajeVale.id }, porcentajeVale);
        }

        // DELETE: api/PorcentajeVales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePorcentajeVale(int id)
        {
            var porcentajeVale = await _context.tbltblPorcentajeVales.FindAsync(id);
            if (porcentajeVale == null)
            {
                return NotFound();
            }

            _context.tbltblPorcentajeVales.Remove(porcentajeVale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PorcentajeValeExists(int id)
        {
            return _context.tbltblPorcentajeVales.Any(e => e.id == id);
        }
    }
}
