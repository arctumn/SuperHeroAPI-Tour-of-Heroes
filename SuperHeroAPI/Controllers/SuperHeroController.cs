using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        public const string ROUTE = "api/v1/SuperHero";


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
   
        [HttpGet("data/{id}")]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            return hero is null ? NotFound() : Ok(hero);
        }
        [HttpGet("data/name/{name}")]
        public async Task<ActionResult<SuperHero>> GetByName(string name)
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            if (name is not "")
            {
                heroes = heroes
                    .FindAll(hero => hero.Name
                        .ToLower()
                        .Contains(name.ToLower()))
                    .ToList();
            }
            return heroes is null ? NotFound() : Ok(heroes);
        }
        [HttpPost]
        public async Task<ActionResult<SuperHero>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(hero);
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
