using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DirectoriosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public DirectoriosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllDirectorios")]
        public IEnumerable<Directorios> GetAllDirectorios()
        {
            return _context.Directorio.Include(x => x.Usuario).ToList();
        }

        [HttpPost]
        [Route("AddDirectorio")]
        public IActionResult AddDirectorio(Directorios directorio)
        {
            _context.Directorio.Add(directorio);
            _context.SaveChanges();
            return Ok();
        }

        //Lo usare para VSCODE
        [HttpGet("{id}")]
        public async Task<ActionResult<Directorios>> GetDirectorio(int id)
        {
            var directorio = await _context.Directorio
                .Include(c => c.Usuario)

                .FirstOrDefaultAsync(c => c.IdDirectorio == id);

            if (directorio == null)
            {
                return NotFound();
            }

            return directorio;
        }
    }

}
