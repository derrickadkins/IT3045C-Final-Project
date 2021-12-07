using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT3045C_Final_Project.Models;


namespace IT3045C_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FavoriteSingerController : ControllerBase
    {
        private readonly Context _context;
        public FavoriteSingerController(Context context)
        {
            _context = context;
        }
        // GET: api/Singer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Singer>>> GetSinge()
        {
            return await _context.Singers.ToListAsync();
        }
        // GET: api/Singer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Singer>>> GetSinger(int id)
        {
            var favoritesinger = await _context.Singers.FindAsync(id);

            if (favoritesinger == null)
            {
                return await _context.Singers.Take(5).ToListAsync();
            }
            return new Singer[] { favoritesinger };
        }
        // PUT: api/Singers/5
        [HttpPut("{id}")]
        private bool SingerExists(int id)
        {
            return _context.Singers.Any(e => e.id == id);
        }
        public async Task<IActionResult> PutSinger(int id, Singer singer)
        {
            if (id != singer.id)
            {
                return BadRequest();
            }
            _context.Entry(singer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SingerExists(id))
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
        // POST: api/Singers
        [HttpPost]
        public async Task<IActionResult> PostSinger(Singer singer)
        {
            _context.Singers.Add(singer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSinger), new { id = singer.id }, singer);
        }
        // DELETE: api/Singers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Singer>> DeleteSinger(int id)
        {
            var singer = await _context.Singers.FindAsync(id);
            if (singer == null)
            {
                return NotFound();
            }
            _context.Singers.Remove(singer);
            await _context.SaveChangesAsync();
            return singer;
        }
    }

}