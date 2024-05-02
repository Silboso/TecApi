using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public HorariosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllHorarios")]
        public IEnumerable<Horarios> GetAllHorarios()
        {
            return _context.Horario.Include(x => x.Directorio).ToList();
        }


        //Lo usare para VSCODE
        [HttpGet("{id}")]
        public async Task<ActionResult<Horarios>> GetHorarios(int id)
        {
            var horarios = await _context.Horario
                .Include(c => c.Directorio)

                .FirstOrDefaultAsync(c => c.IdHorario == id);

            if (horarios == null)
            {
                return NotFound();
            }

            return horarios;
        }
    }
}
