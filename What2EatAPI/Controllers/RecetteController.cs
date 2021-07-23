using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using What2EatAPI;

namespace What2EatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetteController : ControllerBase
    {
        private readonly what2eatContext _context;

        public RecetteController(what2eatContext context)
        {
            _context = context;
        }

        // GET: api/Recette
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recette>>> GetRecettes()
        {
            return await _context.Recettes.ToListAsync();
        }

        // GET: api/Recette/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recette>> GetRecette(int id)
        {
            var recette = await _context.Recettes.FindAsync(id);

            if (recette == null)
            {
                return NotFound();
            }

            return recette;
        }

        // PUT: api/Recette/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecette(int id, Recette recette)
        {
            if (id != recette.IdRecette)
            {
                return BadRequest();
            }

            _context.Entry(recette).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetteExists(id))
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

        // POST: api/Recette
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recette>> PostRecette(Recette recette)
        {
            _context.Recettes.Add(recette);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecette", new { id = recette.IdRecette }, recette);
        }

        // DELETE: api/Recette/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecette(int id)
        {
            var recette = await _context.Recettes.FindAsync(id);
            if (recette == null)
            {
                return NotFound();
            }

            _context.Recettes.Remove(recette);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecetteExists(int id)
        {
            return _context.Recettes.Any(e => e.IdRecette == id);
        }
    }
}
