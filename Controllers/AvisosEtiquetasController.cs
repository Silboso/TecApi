using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvisosEtiquetasController : ControllerBase
    {
        private readonly TecApiContext _context;

        public AvisosEtiquetasController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllAvisosEtiquetas")]
        public IEnumerable<AvisosEtiquetas> GetAllAvisosEtiquetas()
        {
            return _context.AvisoEtiqueta.ToList();
        }
    }
}
