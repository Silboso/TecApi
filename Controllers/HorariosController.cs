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
            return _context.Horario.ToList();
        }
    }
}
