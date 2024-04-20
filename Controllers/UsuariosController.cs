using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
       private readonly TecApiContext _context;

        public UsuariosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllUsuarios")]
        public IEnumerable<Usuarios> GetAllUsuarios()
        {
            return _context.Usuario.ToList();
        }

        [HttpGet]
        [Route("GetUsuario/{id}", Name = "GetUsuario")]
        public IActionResult GetUsuarioByID(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [Route("PostUsuario")]
        public IActionResult PostUsuario(Usuarios usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
            return CreatedAtRoute("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut]
        [Route("PutUsuario/{id}")]
        public IActionResult PutUsuario(int id, Usuarios usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteUsuario/{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.Rol = RolUsuario.Inactivo;
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }
    }

}
