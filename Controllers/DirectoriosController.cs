using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DirectoriosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public DirectoriosController(TecApiContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[Route("GetAllDirectorios")]
        //public IEnumerable<Directorios> GetAllDirectorios()
        //{
        //    return _context.Directorio.Include(x => x.Usuario).ToList();
        //}

        [HttpGet]
        [Route("GetAllDirectorios")]
        public IActionResult GetAllDirectorios()
        {
            var directorios = _context.Directorio
                .Include(c => c.Usuario)
                .ToList();

            if (!directorios.Any())
            {
                return NotFound("No se encontraron directorios.");
            }

            return Ok(directorios);
        }




        [HttpPost]
        [Route("AddDirectorio")]
        public IActionResult AddDirectorio([FromBody] Directorios directorio)
        {
            if (directorio == null)
            {
                return BadRequest("El directorio es nulo");
            }

            _context.Directorio.Add(directorio);
            _context.SaveChanges();
            return Ok();
        }


        //Lo usare para VSCODE
        [HttpGet("{id}")]
        public async Task<ActionResult<Directorios>> GetDirectorio(int id)
        {
            var directorio = await _context.Directorio
                .Include(c => c.Usuario)

                .FirstOrDefaultAsync(c => c.IdDirectorio == id);

            if (directorio == null)
            {
                return NotFound();
            }

            return directorio;
        }



        [HttpPost]
        [Route("AddDirectorioss")]
        public IActionResult AddDirectorioss([FromBody] Directorios directorio)
        {
            if (directorio == null)
            {
                return BadRequest("El directorio es nulo");
            }

            _context.Directorio.Add(directorio);
            _context.SaveChanges();
            return Ok();
        }




    }






}
