using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT3045C_Final_Project.Models;

namespace IT3045C_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivingsController : ControllerBase
    {
        private readonly Context _context;

        public LivingsController(Context context)
        {
            _context = context;
        }

        // GET: api/Livings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Living>>> Getlivings()
        {
            return await _context.livings.ToListAsync();
        }

        // GET: api/Livings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Living>>> GetLiving(int id)
        {
            var living = await _context.livings.FindAsync(id);

            if (living == null)
            {
                return await _context.livings.Take(5).ToListAsync();
            }

            return new Living[] { living };
        }

        // PUT: api/Livings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLiving(int id, Living living)
        {
            if (id != living.Id)
            {
                return BadRequest();
            }

            _context.Entry(living).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivingExists(id))
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

        // POST: api/Livings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Living>> PostLiving(Living living)
        {
            _context.livings.Add(living);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLiving), new { id = living.Id }, living);
        }

        // DELETE: api/Livings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Living>> DeleteLiving(int id)
        {
            var living = await _context.livings.FindAsync(id);
            if (living == null)
            {
                return NotFound();
            }

            _context.livings.Remove(living);
            await _context.SaveChangesAsync();

            return living;
        }

        private bool LivingExists(int id)
        {
            return _context.livings.Any(e => e.Id == id);
        }
    }
}
