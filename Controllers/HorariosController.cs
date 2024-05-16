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
            // Verificar si el directorio existe
            var directorioExistente = _context.Directorio.FirstOrDefault(x => x.IdDirectorio == horario.IdDirectorio);
            if (directorioExistente == null)
            {
                return BadRequest("El directorio no existe");
            }

            // Verificar si el horario ya existe para evitar duplicados
            var horarioExistente = _context.Horario.Any(h => h.IdHorario == horario.IdHorario);
            if (horarioExistente)
            {
                return Conflict("Un horario con este ID ya existe.");
            }

            try
            {
                horario.Directorio = directorioExistente;
                _context.Horario.Add(horario);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al agregar el horario: " + ex.Message);
            }
        }

        //eliminar todos los horarios por id de directorio
        [HttpDelete]
        [Route("DeleteHorarios/{id}")]
        public IActionResult DeleteHorarios(int id)
        {
            var horarios = _context.Horario.Where(x => x.IdDirectorio == id).ToList();
            if (!horarios.Any())
            {
                return NotFound();
            }

            try
            {
                _context.Horario.RemoveRange(horarios);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
