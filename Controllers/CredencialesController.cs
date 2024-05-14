using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredencialesController : ControllerBase
    {
        private readonly TecApiContext _context;

        public CredencialesController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCredenciales")]
        public async Task<IActionResult> GetCredenciales()
        {
            var credenciales = await _context.Credencial.ToListAsync();

            if (!credenciales.Any())
            {
                return NotFound("No se encontraron credenciales.");
            }

            return Ok(credenciales);
        }

        [HttpGet]
        [Route("GetCredencialesWithUsuarios")]
        public async Task<IActionResult> GetCredencialesWithUsuarios()
        {
            var credenciales = await _context.Credencial
                .Include(c => c.Usuario) // Incluir datos del usuario
                .ToListAsync();

            if (!credenciales.Any())
            {
                return NotFound("No se encontraron credenciales.");
            }

            return Ok(credenciales);
        }


        [HttpGet]
        [Route("GetCredencialById/{id}")]
        public async Task<IActionResult> GetCredencialById(int id)
        {
            var credencial = await _context.Credencial.FirstOrDefaultAsync(c => c.IdCredencial == id);

            if (credencial == null)
            {
                return NotFound();
            }

            return Ok(credencial);
        }

        [HttpGet]
        [Route("GetCredencialBySerial/{serial}")]
        public async Task<IActionResult> GetCredencialBySerial(string serial)
        {
            var credencial = await _context.Credencial.FirstOrDefaultAsync(c => c.Serial == serial);

            if (credencial == null)
            {
                return NotFound();
            }

            return Ok(credencial);
        }

        [HttpGet]
        [Route("GetCredencialesByUsuarioId/{idUsuario}")]
        public async Task<IActionResult> GetCredencialesByUsuarioId(int idUsuario)
        {
            var credencial = await _context.Credencial.FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

            if (credencial == null)
            {
                return NotFound();
            }

            return Ok(credencial);
        }

        [HttpGet]
        [Route("GetCredencialByIdWithUsuario/{id}")]
        public async Task<IActionResult> GetCredencialByIdWithUser(int id)
        {
            var credencial = await _context.Credencial
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.IdCredencial == id);

            if (credencial == null)
            {
                return NotFound();
            }

            return Ok(credencial);
        }


        [HttpPost]
        [Route("PostCredencial")]
        public async Task<IActionResult> PostCredencial(Credenciales credencial)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Usuarios usuario = (await _context.Usuario.FindAsync(credencial.IdUsuario))!;

            credencial.Usuario = usuario;
            credencial.Serial = await GenerateSerial(credencial.IdUsuario);

            _context.Credencial.Add(credencial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCredencialByIdWithUser), new { id = credencial.IdCredencial }, credencial);
        }

        [HttpPut]
        [Route("PutCredencial/{id}")]
        public async Task<IActionResult> PutCredencial(int id, Credenciales credencial)
        {
            if (id != credencial.IdCredencial)
            {
                return BadRequest();
            }

            _context.Entry(credencial).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete]
        [Route("DropAllCredenciales")]
        public async Task<IActionResult> DropAllCredenciales()
        {
            _context.Credencial.RemoveRange(_context.Credencial);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        ////////////////////////
        ////////////////////////
        //////// Utils /////////
        ////////////////////////
        ////////////////////////

        private async Task<string> GenerateSerial(int id)
        {
            Usuarios usuario = (await _context.Usuario.FindAsync(id))!;

            int currentYear = DateTime.Now.Year;
            int secuencia = await GetSerialSequence(currentYear) + 1;

            string serial = GenerateIdentifier(usuario, currentYear, secuencia);

            return serial;
        }

        private string GenerateIdentifier(Usuarios usuario, int año, int secuencia)
        {
            string carrera = usuario.Carrera != null && usuario.Carrera.Length >= 3
                ? usuario.Carrera.ToUpper().Substring(0, 3)
                : "UNF";
            string añoStr = año.ToString("D4");
            char primerApellidoInicial =
                !string.IsNullOrEmpty(usuario.ApellidoPaterno) ? usuario.ApellidoPaterno.ToUpper()[0] : 'X';
            char segundoApellidoInicial =
                !string.IsNullOrEmpty(usuario.ApellidoMaterno) ? usuario.ApellidoMaterno.ToUpper()[0] : 'X';
            string[] nombres = usuario.Nombre.Split(' ');
            char primerNombreInicial = !string.IsNullOrEmpty(nombres[0]) ? nombres[0].ToUpper()[0] : 'X';
            char segundoNombreInicial =
                nombres.Length > 1 && !string.IsNullOrEmpty(nombres[1]) ? nombres[1].ToUpper()[0] : 'X';
            string secuenciaStr = secuencia.ToString("D3");
            string rolStr = usuario.Rol.ToString().ToUpper();
            rolStr = rolStr.Length >= 3
                ? rolStr.Substring(0, 3)
                : rolStr.PadRight(3, 'X');

            return
                $"{carrera}{añoStr}{primerApellidoInicial}{segundoApellidoInicial}{primerNombreInicial}{segundoNombreInicial}{secuenciaStr}{rolStr}";
        }

        private async Task<int> GetSerialSequence(int year)
        {
            var yearStr = year.ToString("D4");
            var serialesDelAño = await _context.Credencial
                .Where(c => c.Serial.Contains(yearStr))
                .Select(c => c.Serial)
                .ToListAsync();

            int maxSecuencia = 0;
            foreach (var serial in serialesDelAño)
            {
                int index = serial.IndexOf(yearStr, StringComparison.Ordinal) + 8;
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
    }
}