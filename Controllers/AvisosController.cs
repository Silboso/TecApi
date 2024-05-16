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
            var comentarios= _context.Comentario.Include(c => c.Usuario)
                .Where(c => c.IdAviso == id)
                .ToList();

            //Silencia el token con null
            foreach (var comentario in comentarios)
            {
                comentario.Usuario.Token = null;
            }
            //Si comentarios es nulo, regresa un mensaje de error
            if (comentarios == null)
            {
               return null;
            }
            return comentarios;
        }


        [HttpPost]
        [Route("CreateAviso")]
        public IActionResult CreateAviso(Avisos aviso)
        {
            // Validar que el IdAviso no exista ya en la base de datos
            if (_context.Aviso.Any(a => a.IdAviso == aviso.IdAviso))
            {
                return BadRequest("El IdAviso ya existe.");
            }

            // Validar que el IdUsuario exista en la base de datos
            var usuarioExiste = _context.Usuario.Any(u => u.IdUsuario == aviso.IdUsuario);
            if (!usuarioExiste)
            {
                return BadRequest("El IdUsuario no existe.");
            }

            // Validar que la etiqueta ya existe en la base de datos
            var etiquetaExistente = _context.Etiqueta.SingleOrDefault(e => e.IdEtiqueta == aviso.Etiqueta.IdEtiqueta);
            if (etiquetaExistente != null)
            {
                aviso.Etiqueta = etiquetaExistente;
            }

            // Validar que el usuario ya existe en la base de datos
            var usuarioExistente = _context.Usuario.SingleOrDefault(u => u.IdUsuario == aviso.IdUsuario);
            if (usuarioExistente != null)
            {
                aviso.Usuario = usuarioExistente;
            }

            _context.Aviso.Add(aviso);
            _context.SaveChanges();
            return CreatedAtRoute("GetAviso", new { id = aviso.IdAviso }, aviso);
        }


    }
}
