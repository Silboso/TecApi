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

        //Metodo add pero que verifique si el directorio existe
        [HttpPost]
        [Route("AddPin")]
        public IActionResult AddPin([FromBody] Pines pin)
        {
            // Verificar si el directorio existe
            var directorioExistente = _context.Directorio.FirstOrDefault(x => x.IdDirectorio == pin.IdDirectorio);
            if (directorioExistente == null)
            {
                return BadRequest("El directorio no existe");
            }

            try
            {
                pin.Directorio = directorioExistente;  // Asociar el directorio existente al nuevo pin
                _context.Pin.Add(pin);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //eliminar pines por iddirectorio
        [HttpDelete]
        [Route("DeletePin/{id}")]
        public IActionResult DeletePin(int id)
        {
            var pin = _context.Pin.FirstOrDefault(x => x.IdPin == id);
            if (pin == null)
            {
                return NotFound();
            }

            _context.Pin.Remove(pin);
            _context.SaveChanges();
            return Ok();
        }

    }
}
