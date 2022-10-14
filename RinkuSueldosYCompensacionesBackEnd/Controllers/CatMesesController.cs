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
    public class CatMesesController : ControllerBase
    {
        private readonly DataContext _context;

        public CatMesesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CatMeses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatMes>>> GettbltblCatMeses()
        {
            return await _context.tbltblCatMeses.ToListAsync();
        }

        // GET: api/CatMeses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatMes>> GetCatMes(int id)
        {
            var catMes = await _context.tbltblCatMeses.FindAsync(id);

            if (catMes == null)
            {
                return NotFound();
            }

            return catMes;
        }

    }
}
