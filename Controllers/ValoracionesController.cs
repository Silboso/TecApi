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
        public IActionResult AddValoracion(Valoraciones valoracion)
        {
            _context.Valoracion.Add(valoracion);
            _context.SaveChanges();
            return Ok(valoracion);
        }
    }
}
