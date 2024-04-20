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

        [HttpGet(Name = "GetAll")]
        public IEnumerable<ModeloUsuario> Get()
        {
            return _context.Usuarios.ToList();
        }

        [HttpGet("{id}")]
        public ModeloUsuario GetById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        [HttpPost]
        public IActionResult Post(ModeloUsuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return CreatedAtRoute("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ModeloUsuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }
    }

}
