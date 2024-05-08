using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConductoresController : ControllerBase
    {
        private readonly TecApiContext _context;

        public ConductoresController(TecApiContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[Route("GetAllConductores")]
        //public IEnumerable<Conductores> GetAllConductores()
        //{
        //    return _context.Conductor.Include(x => x.Usuario).ToList();


        //}

        [HttpGet]
        [Route("GetAllConductores")]
        public IActionResult GetAllConductores()
        {
            var conductores = _context.Conductor
                .Include(c => c.Usuario)
                .ToList();

            if (!conductores.Any())
            {
                return NotFound("No se encontraron conductores.");
            }

            return Ok(conductores);
        }



        //LO USO AHORA EN VSCODE
        [HttpGet("{id}")]
        public async Task<ActionResult<Conductores>> GetConductor(int id)
        {
            var conductor = await _context.Conductor
                .Include(c => c.Usuario)

                .FirstOrDefaultAsync(c => c.IdConductor == id);

            if (conductor == null)
            {
                return NotFound();
            }

            return conductor;
        }








        [HttpGet]
        [Route("GetConductor/{id}", Name = "GetConductor")]
        public IActionResult GetConductorByID(int id)
        {
            var conductor = _context.Conductor.Include(x => x.Usuario).FirstOrDefault(x => x.IdConductor == id);
            if (conductor == null)
            {
                return NotFound();
            }
            return Ok(conductor);
        }

        [HttpPost]
        [Route("AddConductor")]
        public IActionResult AddConductor([FromBody] Conductores conductor)
        {
            // Asegurarse de que el usuario existe
            var usuarioExistente = _context.Usuario.FirstOrDefault(u => u.IdUsuario == conductor.IdUsuario);
            if (usuarioExistente == null)
            {
                return BadRequest("Usuario no existe");
            }

            try
            {
                conductor.Usuario = usuarioExistente;  // Asociar el usuario existente al nuevo conductor
                _context.Conductor.Add(conductor);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al agregar el conductor: " + ex.Message);
            }
        }







        [HttpGet]
        [Route("GetAllConductoresDetails")]
        public async Task<IActionResult> GetAllConductoresDetails()
        {
            var conductores = await _context.Conductor
                .Include(conductor => conductor.Usuario)
                .Select(conductor => new
                {
                    ConductorId = conductor.IdConductor,
                    UsuarioId = conductor.IdUsuario,
                    Telefono = conductor.NoTelefono,
                    UsuarioNombre = conductor.Usuario.Nombre, // Asegúrate de tener un campo Nombre en la entidad Usuario
                    Directorio = _context.Directorio
                        .Where(d => d.IdUsuario == conductor.IdUsuario)
                        .Select(d => new {
                            Marca = d.Marca,
                            Modelo = d.Modelo,
                            Color = d.Color,
                            Horarios = _context.Horario.Where(h => h.IdDirectorio == d.IdDirectorio).ToList(),
                            Pines = _context.Pin.Where(p => p.IdDirectorio == d.IdDirectorio).ToList()
                        }).FirstOrDefault()
                })
                .ToListAsync();


            if (conductores == null || !conductores.Any())
            {
                return NotFound("No se encontraron conductores con información completa.");
            }
            return Ok(conductores);
        }


        //Metodo para agregar conductores de manera correcta
        [HttpPost]
        [Route("AddConductorDetails")]
        public async Task<IActionResult> AddConductorDetails([FromBody] Conductores conductor)
        {
            if (conductor == null)
            {
                return BadRequest("El conductor es nulo");
            }

            _context.Conductor.Add(conductor);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
