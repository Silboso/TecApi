using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValoracionesController : ControllerBase
    {
        private readonly TecApiContext _context;

        public ValoracionesController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetValoracion/{idAlimento}", Name = "GetValoracion")]
        public IActionResult GetValoracionByAlimentoID(int idAlimento)
        {
            // Incluye el usuario en la consulta
            var valoraciones = _context.Valoracion
                                       .Where(v => v.IdAlimento == idAlimento)
                                       .Include(v => v.Usuario) // Asegúrate de tener una propiedad Usuario en tu modelo de Valoracion
                                       .ToList();

            if (!valoraciones.Any()) // Usar Any() es más eficiente que contar los elementos
            {
                return NotFound();
            }

            return Ok(valoraciones);
        }


        [HttpPost]
        [Route("AddValoracion")]
        public IActionResult AddValoracion([FromBody] Valoraciones valoracion)
        {
            if (valoracion == null)
            {
                return BadRequest("El objeto de valoración no puede ser nulo.");
            }

            // Verificar que el usuario y el alimento existen en la base de datos
            var usuarioExistente = _context.Usuario.Find(valoracion.Usuario.IdUsuario);
            var alimentoExistente = _context.Alimento.Find(valoracion.Alimento.IdAlimento);

            // Verificar si el usuario y el alimento existen
            if (usuarioExistente == null)
            {
                return BadRequest($"No existe un usuario con el ID {valoracion.Usuario.IdUsuario}.");
            }

            if (alimentoExistente == null)
            {
                return BadRequest($"No existe un alimento con el ID {valoracion.Alimento.IdAlimento}.");
            }

            // Asociar las referencias existentes con la valoración
            valoracion.Usuario = usuarioExistente;
            valoracion.Alimento = alimentoExistente;

            _context.Valoracion.Add(valoracion);
            _context.SaveChanges();
            return Ok(valoracion);
        }





    }
}
