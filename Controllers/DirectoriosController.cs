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

            //Cambia el token a null

            foreach (var directorio in directorios)
            {
                directorio.Usuario.Token = null;
            }

            if (!directorios.Any())
            {
                return NotFound("No se encontraron directorios.");
            }

            return Ok(directorios);
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


        /*Metodo add similar a este
         *       [HttpPost]
        [Route("AddConductor")]
        public IActionResult AddConductor([FromBody] Conductores conductor)
        {
            // Asegurarse de que el usuario existe
            var usuarioExistente = _context.Usuario.FirstOrDefault(u => u.IdUsuario == conductor.IdUsuario);
            if (usuarioExistente == null)
            {
                return BadRequest("Usuario no existe");
            }

            try
            {
                conductor.Usuario = usuarioExistente;  // Asociar el usuario existente al nuevo conductor
                _context.Conductor.Add(conductor);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al agregar el conductor: " + ex.Message);
            }
        }

    */

        [HttpPost]
        [Route("AddDirectorio")]
        public IActionResult AddDirectorio([FromBody] Directorios directorio)
        {
            // Asegurarse de que el usuario existe
            var usuarioExistente = _context.Usuario.FirstOrDefault(u => u.IdUsuario == directorio.IdUsuario);
            if (usuarioExistente == null)
            {
                return BadRequest("Usuario no existe");
            }

            try
            {
                directorio.Usuario = usuarioExistente;  // Asociar el usuario existente al nuevo directorio
                _context.Directorio.Add(directorio);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al agregar el directorio: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteDirectorio/{id}")]
        public IActionResult DeleteDirectorio(int id)
        {
            var directorio = _context.Directorio.FirstOrDefault(x => x.IdDirectorio == id);
            if (directorio == null)
            {
                return NotFound();
            }

            try
            {
                _context.Directorio.Remove(directorio);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar el directorio: " + ex.Message);
            }
        }

    


    }






}
