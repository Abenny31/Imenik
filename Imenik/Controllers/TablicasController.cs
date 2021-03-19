using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Imenik.Data;
using Imenik.Models;

namespace Imenik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablicasController : ControllerBase
    {
        private readonly ImenikDbContext _context;
        


        public TablicasController(ImenikDbContext context)
        {
            _context = context;
        }

        // GET: api/Tablicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tablica>>> GetTablicas()
        {
            return await _context.Tablicas.ToListAsync();
        }
       

        // GET: api/Tablicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tablica>> GetTablica(string pretraga)
        {
           
            var tablica = await _context.Tablicas.FindAsync(pretraga);

            if (tablica == null)
            {
                return NotFound();
            }

            return tablica;
        }

        // PUT: api/Tablicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTablica(int id, Tablica tablica)
        {
            if (id != tablica.ID)
            {
                return BadRequest();
            }

            _context.Entry(tablica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TablicaExists(id))
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

        // POST: api/Tablicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tablica>> PostTablica(Tablica tablica)
        {
            _context.Tablicas.Add(tablica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTablica", new { id = tablica.ID }, tablica);
        }

        // DELETE: api/Tablicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTablica(int id)
        {
            var tablica = await _context.Tablicas.FindAsync(id);
            if (tablica == null)
            {
                return NotFound();
            }

            _context.Tablicas.Remove(tablica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TablicaExists(int id)
        {
            return _context.Tablicas.Any(e => e.ID == id);
        }
    }
}
