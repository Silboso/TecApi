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
            return _context.Directorio.ToList();
        }
    }
}
