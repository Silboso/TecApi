using System.Reflection;
using System.Text.Json;
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
        public IActionResult GetUsuarioById(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpGet]
        [Route("GetUsuarioByUid/{uid}")]
        public async Task<IActionResult> GetUsuarioByUid(string uid)
        {
            // Remplace the "Token" property with the "Uid" property when actualice database
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Token == uid);
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
        public IActionResult PutUsuario(int id, [FromBody] Usuarios updatedUsuario)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Foto = updatedUsuario.Foto ?? usuario.Foto;
            usuario.Nombre = updatedUsuario.Nombre ?? usuario.Nombre;
            usuario.ApellidoPaterno = updatedUsuario.ApellidoPaterno ?? usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = updatedUsuario.ApellidoMaterno ?? usuario.ApellidoMaterno;
            usuario.Matricula = updatedUsuario.Matricula ?? usuario.Matricula;
            usuario.Token = updatedUsuario.Token ?? usuario.Token;
            usuario.Carrera = updatedUsuario.Carrera ?? usuario.Carrera;
            usuario.Semestre = updatedUsuario.Semestre ?? usuario.Semestre;
            usuario.Sexo = updatedUsuario.Sexo ?? usuario.Sexo;
            usuario.Rol = updatedUsuario.Rol ?? usuario.Rol;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuario.Any(e => e.IdUsuario == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(usuario);
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
        
        [HttpDelete]
        [Route("DeletePermanentlyUsuario/{id}")]
        public IActionResult DeletePermanentlyUsuario(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }
    }
}