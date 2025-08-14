using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sharper.Data;
using sharper.Models;

namespace sharper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : Controller
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            var heros = await _context.SuperHeroes.ToListAsync();
            return Ok(heros);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null) return NotFound();

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            var heros = await _context.SuperHeroes.ToListAsync();
            return Ok(heros);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHero request)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null) return NotFound();

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;

            await _context.SaveChangesAsync();

            var heros = await _context.SuperHeroes.ToListAsync();
            return Ok(heros);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null) return NotFound();

            _context.SuperHeroes.Remove(hero);

            await _context.SaveChangesAsync();

            var heros = await _context.SuperHeroes.ToListAsync();

            return Ok(heros);
        }
    }
}
