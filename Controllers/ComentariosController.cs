using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComentariosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public ComentariosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllComentarios")]
        public IEnumerable<Comentarios> GetAllComentarios()
        {
            return _context.Comentario.ToList();
        }
    }
}
