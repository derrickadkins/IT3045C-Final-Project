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

    public class FavoriteFoodController : ControllerBase
    {
        private readonly Context _context;

        public FavoriteFoodController(Context context)
        {
            _context = context;
        }
        // GET: api/Food
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
        {
            return await _context.Foods.ToListAsync();
        }
        // get: api/hobbies
        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<Food>>> GetFood(int id)
        {
            var favoritefood = await _context.Foods.FindAsync(id);

            if (favoritefood == null)
            {
                return await _context.Foods.Take(5).ToListAsync();
            }

            return new Food[] { favoritefood };

        }

        // put: api/foods/5
        [HttpPut("{id}")]

        public async Task<IActionResult> PutFood(int id, Food food)
        { 
            if(id != food.id)
            {
                return BadRequest();
            }
            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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


        [HttpPost]
        public async Task<IActionResult> PostFood(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFood), new { id = food.id }, food);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Food>> DeleteFood(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            { 
                return NotFound();
            
            }
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            return food;

        }
        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.id == id);
        }
    }

}
