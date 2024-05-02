using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    public class ValoracionesController : ControllerBase
    {
        private readonly TecApiContext _context;

        public ValoracionesController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetValoracion/{id}", Name = "GetValoracion")]
        public IActionResult GetValoracionByID(int id)
        {
            var valoracion = _context.Valoracion.FirstOrDefault(v => v.IdValoracion == id);
            if (valoracion == null)
            {
                return NotFound();
            }
            return Ok(valoracion);
        }

        [HttpPost]
        [Route("AddValoracion")]
        public IActionResult AddValoracion([FromBody] Valoraciones valoracion)
        {
            if (valoracion == null)
            {
                return BadRequest("El objeto de valoración no puede ser nulo.");
            }

            // Asegurarse de que Entity Framework no intente procesar la entidad Usuario como una nueva inserción
            if (valoracion.IdUsuario != 0)
            {
                _context.Entry(_context.Usuario.Find(valoracion.IdUsuario)).State = EntityState.Unchanged;
            }

            // Verificar si el alimento existe
            var alimento = _context.Alimento.Find(valoracion.IdAlimento);
            if (alimento == null)
            {
                return BadRequest($"No existe un alimento con el ID {valoracion.IdAlimento}.");
            }

            _context.Valoracion.Add(valoracion);
            _context.SaveChanges();
            return Ok(valoracion);
        }



    }
}
