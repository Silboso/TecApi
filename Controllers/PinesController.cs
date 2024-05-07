using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PinesController : ControllerBase
    {
        private readonly TecApiContext _context;

        public PinesController(TecApiContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[Route("GetAllPines")]
        //public IEnumerable<Pines> GetAllPines()
        //{
        //    return _context.Pin.Include(x => x.Directorio).ToList();
        //}



        [HttpGet]
        [Route("GetAllPines")]
        public IActionResult GetAllPines()
        {
            var pines = _context.Pin
                .Include(c => c.Directorio)
                .ToList();

            if (!pines.Any())
            {
                return NotFound("No se encontraron pines.");
            }

            return Ok(pines);
        }





        //Lo usare para VSCODE
        [HttpGet("{id}")]
        public async Task<ActionResult<Pines>> GetPines(int id)
        {
            var pines = await _context.Pin
                .Include(c => c.Directorio)

                .FirstOrDefaultAsync(c => c.IdPin == id);

            if (pines == null)
            {
                return NotFound();
            }

            return pines;
        }

        //Agregar Pin
        [HttpPost]
        [Route("AddPin")]
        public IActionResult AddPin([FromBody] Pines pin)
        {
            if (pin == null)
            {
                return BadRequest("El pin es nulo");
            }

            _context.Pin.Add(pin);
            _context.SaveChanges();
            return Ok();
        }
    }
}
