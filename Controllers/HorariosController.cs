using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public HorariosController(TecApiContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[Route("GetAllHorarios")]
        //public IEnumerable<Horarios> GetAllHorarios()
        //{
        //    return _context.Horario.Include(x => x.Directorio).ToList();
        //}

        [HttpGet]
        [Route("GetAllHorarios")]
        public IActionResult GetAllHorarios()
        {
            var horarios = _context.Horario
                .Include(c => c.Directorio)
                .ToList();

            if (!horarios.Any())
            {
                return NotFound("No se encontraron horarios.");
            }

            return Ok(horarios);
        }



        //Lo usare para VSCODE
        [HttpGet("{id}")]
        public async Task<ActionResult<Horarios>> GetHorarios(int id)
        {
            var horarios = await _context.Horario
                .Include(c => c.Directorio)

                .FirstOrDefaultAsync(c => c.IdHorario == id);

            if (horarios == null)
            {
                return NotFound();
            }

            return horarios;
        }

        //Agregar Horario
        [HttpPost]
        [Route("AddHorario")]
        public IActionResult AddHorario([FromBody] Horarios horario)
        {
            if (horario == null)
            {
                return BadRequest("El horario es nulo");
            }

            _context.Horario.Add(horario);
            _context.SaveChanges();
            return Ok();
        }
    }
}
