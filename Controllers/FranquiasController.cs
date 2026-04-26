using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CacauShowApi325219722.Data;
using CacauShowApi325219722.Models;

namespace CacauShowApi325219722.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FranquiasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FranquiasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Franquia>>> Get()
        {
            return await _context.Franquias.ToListAsync();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Franquia>> GetById(int id)
        {
            var franquia = await _context.Franquias.FindAsync(id);

            if (franquia == null)
               return NotFound();
               return franquia;
        }
       [HttpPost]
public async Task<ActionResult<Franquia>> Post(Franquia franquia)
{
    _context.Franquias.Add(franquia);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetById), new { id = franquia.Id }, franquia);
}
    }

}