using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredencialesController : ControllerBase
    {
        private readonly TecApiContext _context;

        public CredencialesController(TecApiContext context)
        {
            _context = context;
        }

        //obtener todas las credenciales metodo basico
        [HttpGet]
        [Route("GetAllCredenciales")]
        public IActionResult GetAllCredenciales()
        {
            var credenciales = _context.Credencial.ToList();
            if (credenciales == null || !credenciales.Any())
            {
                return NotFound("No se encontraron credenciales.");
            }
            return Ok(credenciales);
        }

        //get credencial incluyendo el usuario
        [HttpGet]
        [Route("GetCredencialesConUsuario")]
        public IActionResult GetAllCredencialesWithUsers()
        {
            var credenciales = _context.Credencial
                                    .Include(c => c.Usuario)  // Incluir datos del usuario
                                    .ToList();
            if (credenciales == null || !credenciales.Any())
            {
                return NotFound("No se encontraron credenciales.");
            }
            return Ok(credenciales);
        }


        // Método GET para recuperar una credencial por ID
        [HttpGet]
        [Route("GetCredencialesPorId/{id}")]
        public IActionResult GetCredencialByID(int id)
        {
            var credencial = _context.Credencial.FirstOrDefault(c => c.Id == id);
            if (credencial == null)
            {
                return NotFound();
            }
            return Ok(credencial);
        }

        [HttpGet]
        [Route("GetCredencialesPorIdConUsuario/{id}")]
        public IActionResult GetCredencialByIdWithUser(int id)
        {
            var credencial = _context.Credencial
                                      .Include(c => c.Usuario)  // Incluir datos del usuario
                                      .FirstOrDefault(c => c.Id == id);

            if (credencial == null)
            {
                return NotFound();
            }

            return Ok(credencial);
        }


        [HttpPost]
        public IActionResult PostCredencial(Credenciales credencial)
        {
            if (ModelState.IsValid)
            {
                // Buscar el usuario correspondiente en la base de datos
                var usuario = _context.Usuario.Find(credencial.IdUsuario);
                if (usuario == null)
                {
                    // Devolver una respuesta HTTP 404 Not Found si el usuario no existe
                    return NotFound("El usuario especificado no existe.");
                }

                // Asociar el usuario encontrado con la credencial
                credencial.Usuario = usuario;

                // Agregar la nueva credencial al contexto de base de datos
                _context.Credencial.Add(credencial);
                _context.SaveChanges();

                // Devolver una respuesta HTTP 201 Created con la nueva credencial en el cuerpo de la respuesta
                return CreatedAtAction(nameof(GetCredencialByIdWithUser), new { id = credencial.Id }, credencial);
            }
            else
            {
                // Si el modelo no es válido, devolver una respuesta HTTP 400 Bad Request
                return BadRequest(ModelState);
            }
        }
    }
}