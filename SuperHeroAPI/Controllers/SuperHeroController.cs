using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {


        private readonly DataContext _context;

        public SuperHeroController(DataContext Context)
        {
            _context = Context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAll()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            return hero is null ? NotFound() : Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await _context.SuperHeroes.FindAsync(request.ID);
            if (hero is null) return NotFound();
            hero.Update(request);

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null) return NotFound();
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteAll()
        {
            var data = await _context.SuperHeroes.ToListAsync();
            if (data is null) return Ok("Table was Empty");
            _context.SuperHeroes.RemoveRange(data);
            await _context.SaveChangesAsync();
            return Ok("Emptied Table");
        }
    }
}
