using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Incidents.Data;
using Service_Incidents.Models;

namespace Service_Incidents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IncidentsDbContext _context;

        public TypesController(IncidentsDbContext context)
        {
            _context = context;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Types>>> GetTypes()
        {
            List<Types> types = await _context.Types.ToListAsync();
            return Ok(types);
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Types>> GetTypes(int id)
        {
       
            var types = await _context.Types.FindAsync(id);

            if (types == null)
            {
                return NotFound();
            }

            return Ok(types);
        }

        // PUT: api/Types/5
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutTypes(int id, Types types)
        {
            if (id != types.INCD_TYPE_ID)
            {
                return BadRequest();
            }

            _context.Entry(types).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Types
        [HttpPost]
        public async Task<ActionResult<Types>> PostTypes(Types types)
        {
          if (_context.Types == null)
          {
              return Problem("Entity set 'IncidentsDbContext.Types'  is null.");
          }
            _context.Types.Add(types);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypes", new { id = types.INCD_TYPE_ID }, types);
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypes(int id)
        {
            if (_context.Types == null)
            {
                return NotFound();
            }
            var types = await _context.Types.FindAsync(id);
            if (types == null)
            {
                return NotFound();
            }

            _context.Types.Remove(types);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypesExists(int id)
        {
            return (_context.Types?.Any(e => e.INCD_TYPE_ID == id)).GetValueOrDefault();
        }
    }
}
