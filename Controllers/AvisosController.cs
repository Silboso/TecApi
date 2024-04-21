using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvisosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public AvisosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllAvisos")]
        public IEnumerable<Avisos> GetAllAvisos()
        {
            return _context.Aviso.ToList();
        }

        [HttpGet]
        [Route("GetAviso/{id}", Name = "GetAviso")]
        public IActionResult GetAvisoByID(int id)
        {
            var aviso = _context.Aviso.Find(id);
            if (aviso == null)
            {
                return NotFound();
            }
            return Ok(aviso);
        }
    }
}
