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

        //GENERAR SERIAL PARA CREDENCIAL
        [HttpGet]
        [Route("GenerateSerial/{id}")]
        public IActionResult GenerateSerial(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            int añoActual = DateTime.Now.Year;
            //int secuencia = SecuenciasSerial(añoActual); // Aún no se cómo mantener las secuencias
            int secuencia = 1;

            string serial = GenerarIdentificador(usuario, añoActual, secuencia);
            return Ok(new { Serial = serial });
        }

        private string GenerarIdentificador(Usuarios usuario, int año, int secuencia)
        {
            string carrera = usuario.Carrera != null && usuario.Carrera.Length >= 3 ? usuario.Carrera.ToUpper().Substring(0, 3) : "UNF";
            string añoStr = año.ToString("D4");
            char primerApellidoInicial = !string.IsNullOrEmpty(usuario.ApellidoPaterno) ? usuario.ApellidoPaterno.ToUpper()[0] : 'X';
            char segundoApellidoInicial = !string.IsNullOrEmpty(usuario.ApellidoMaterno) ? usuario.ApellidoMaterno.ToUpper()[0] : 'X';
            string[] nombres = usuario.Nombre.Split(' ');
            char primerNombreInicial = !string.IsNullOrEmpty(nombres[0]) ? nombres[0].ToUpper()[0] : 'X';
            char segundoNombreInicial = nombres.Length > 1 && !string.IsNullOrEmpty(nombres[1]) ? nombres[1].ToUpper()[0] : 'X';
            string secuenciaStr = secuencia.ToString("D3");
            string rolStr = usuario.Rol.ToString().Length >= 3 ? usuario.Rol.ToString().ToUpper().Substring(0, 3) : "ROL";

            return $"{carrera}{añoStr}{primerApellidoInicial}{segundoApellidoInicial}{primerNombreInicial}{segundoNombreInicial}{secuenciaStr}{rolStr}";
        }

        private int SecuenciasSerial(int year)
        {
            // Implementar la lógica para obtener la siguiente secuencia
            // Esto podría implicar acceder a una tabla de la base de datos que mantenga el recuento de las secuencias por año
            return 1; // Valor temporal, implementar la lógica adecuada
        }
        //LMAO
    }

}
