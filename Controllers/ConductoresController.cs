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

        [HttpGet]
        [Route("GetAllConductores")]
        public IEnumerable<Conductores> GetAllConductores()
        {
            return _context.Conductor.Include(x => x.Usuario).ToList();
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
        public IActionResult AddConductor(Conductores conductor)
        {
            _context.Conductor.Add(conductor);
            _context.SaveChanges();
            return Ok();
        }



    }
}
