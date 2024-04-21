using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarritosController : Controller
    {
        private readonly TecApiContext _context;

        public CarritosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllCarritos")]
        public IEnumerable<Carritos> GetAllCarritos()
        {
            return _context.Carrito.Include(c => c.Alimento).ToList();
        }
    }
}
