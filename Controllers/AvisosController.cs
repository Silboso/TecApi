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
            IEnumerable<Avisos> avisos = _context.Aviso.Include(a => a.Usuario)
                .Include(a => a.Etiqueta)
                .ToList();

            //Antes de regresar el aviso, silencia el token con null
            foreach (var aviso in avisos)
            {
                aviso.Usuario.Token = null;
            }

            return avisos;
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
            var aviso = _context.Aviso.Include(a => a.Usuario)
                .Include(a => a.Etiqueta)
                .FirstOrDefault(a => a.IdAviso == id);

            //Antes de regresar el aviso, silencia el token con null
            if (aviso != null)
            {
                aviso.Usuario.Token = null;
            }

            if (aviso == null)
            {
                return NotFound();
            }
            return Ok(aviso);
        }

        [HttpGet]
        [Route("GetAvisosByEtiqueta/{id}", Name = "GetAvisosByEtiqueta")]
        public IEnumerable<Avisos> GetAvisosByEtiqueta(int id)
        {
            return _context.Aviso.Include(a => a.Usuario)
                .Include(a => a.Etiqueta)
                .Where(a => a.Etiqueta.IdEtiqueta == id)
                .ToList();
        }

        //Obten los comentarios de un aviso
        [HttpGet]
        [Route("GetComentariosByAviso/{id}", Name = "GetComentariosByAviso")]
        public IEnumerable<Comentarios> GetComentariosByAviso(int id)
        {
            return _context.Comentario.Include(c => c.Usuario)
                .Where(c => c.IdAviso == id)
                .ToList();
        }
    }
}
