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
            int secuencia = SecuenciasSerial(añoActual) + 1;

            string serial = GenerarIdentificador(usuario, añoActual, secuencia);

            var credencial = new Credenciales
            {
                IdUsuario = id,
                Serial = serial,
                Usuario = usuario
            };

            _context.Credencial.Add(credencial);
            _context.SaveChanges();

            return CreatedAtAction(nameof(CredencialesController.GetCredencialByIdWithUser), "Credenciales", new { id = credencial.Id }, credencial);//es el metodo de credenciales
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
            string rolStr = usuario.Rol.ToString().ToUpper();  // Convertir el nombre del rol a mayúsculas
            rolStr = rolStr.Length >= 3 ? rolStr.Substring(0, 3) : rolStr.PadRight(3, 'X');  // Tomar solo las primeras 3 letras, llenar con 'X' si es necesario

            return $"{carrera}{añoStr}{primerApellidoInicial}{segundoApellidoInicial}{primerNombreInicial}{segundoNombreInicial}{secuenciaStr}{rolStr}";
        }

        private int SecuenciasSerial(int year)
        {
            var yearStr = year.ToString("D4");
            var serialesDelAño = _context.Credencial
                .Where(c => c.Serial.Contains(yearStr))
                .Select(c => c.Serial)
                .ToList();

            int maxSecuencia = 0;
            foreach (var serial in serialesDelAño)
            {
                // Asumiendo que el formato del serial incluye la secuencia como los tres dígitos antes del rol
                int index = serial.IndexOf(yearStr) + 8; // 'Carrera(3)' + 'Año(4)' + 'Inicial(1)'
                if (index + 3 <= serial.Length)
                {
                    string secuenciaStr = serial.Substring(index, 3);
                    if (int.TryParse(secuenciaStr, out int secuencia))
                    {
                        if (secuencia > maxSecuencia)
                        {
                            maxSecuencia = secuencia;
                        }
                    }
                }
            }
            return maxSecuencia;
        }
        //LMAO
    }

}
