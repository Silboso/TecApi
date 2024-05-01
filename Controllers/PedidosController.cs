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

        [HttpGet]
        [Route("GetPedido/{id}", Name = "GetPedido")]
        public IActionResult GetPedidoByID(int id)
        {
            var pedido = _context.PedidoEncabezado.Include(p => p.Usuario).FirstOrDefault(p => p.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost]
        [Route("AddPedido")]
        public IActionResult AddPedido(PedidosEncabezados pedido)
        {
            _context.PedidoEncabezado.Add(pedido);
            _context.SaveChanges();
            return Ok(pedido);
        }

        [HttpPut]
        [Route("UpdatePedido/{id}")]
        public IActionResult UpdatePedido(int id, PedidosEncabezados pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest();
            }
            _context.Entry(pedido).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(pedido);
        }

        [HttpGet]
        [Route("GetPedidoDetalle/{id}", Name = "GetPedidoDetalle")]
        public IActionResult GetPedidoDetalleByID(int id)
        {
            var pedidoDetalle = _context.PedidoDetalle.Include(p => p.Alimento).FirstOrDefault(p => p.IdPedido == id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }
            return Ok(pedidoDetalle);
        }

        [HttpPost]
        [Route("AddPedidoDetalle")]
        public IActionResult AddPedidoDetalle(PedidosDetalles pedidoDetalle)
        {
            _context.PedidoDetalle.Add(pedidoDetalle);
            _context.SaveChanges();
            return Ok(pedidoDetalle);
        }
    }
}
