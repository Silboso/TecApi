using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public PedidosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllPedidos")]
        public IEnumerable<PedidosEncabezados> GetAllPedidos()
        {
            return _context.PedidoEncabezado.ToList();
        }
    }
}
