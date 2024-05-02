using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PinesController : ControllerBase
    {
        private readonly TecApiContext _context;

        public PinesController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllPines")]
        public IEnumerable<Pines> GetAllPines()
        {
            return _context.Pin.Include(x => x.Directorio).ToList();
        }

        //Lo usare para VSCODE
        [HttpGet("{id}")]
        public async Task<ActionResult<Pines>> GetPines(int id)
        {
            var pines = await _context.Pin
                .Include(c => c.Directorio)

                .FirstOrDefaultAsync(c => c.IdPin == id);

            if (pines == null)
            {
                return NotFound();
            }

            return pines;
        }
    }
}
