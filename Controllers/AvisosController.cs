using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvisosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public AvisosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllAvisos")]
        public IEnumerable<Avisos> GetAllAvisos()
        {
            return _context.Aviso.Include(a => a.Usuario).ToList();
        }

        [HttpGet]
        [Route("GetAvisosId")]
        public IEnumerable<Avisos> GetAvisosId()
        {
            //Solo regresa los id de los avisos
            return _context.Aviso.Select(a => new Avisos { IdAviso = a.IdAviso }).ToList();
        }

        [HttpGet]
        [Route("GetAviso/{id}", Name = "GetAviso")]
        public IActionResult GetAvisoByID(int id)
        {
            var aviso = _context.Aviso.Include(a => a.Usuario).FirstOrDefault(a => a.IdAviso == id);
            if (aviso == null)
            {
                return NotFound();
            }
            return Ok(aviso);
        }
    }
}
