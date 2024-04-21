using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    public class ValoracionesController : ControllerBase
    {
        private readonly TecApiContext _context;

        public ValoracionesController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllValoraciones")]
        public IEnumerable<Valoraciones> GetAllValoraciones()
        {
            return _context.Valoracion.ToList();
        }
    }
}
