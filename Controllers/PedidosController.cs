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
        [Route("GetPedido/{userId}", Name = "GetPedidoByUserId")]
        public IActionResult GetPedidoByUserID(int userId)
        {
            var pedido = _context.PedidoEncabezado
                                 .Include(p => p.Usuario)
                                 .FirstOrDefault(p => p.IdUsuario == userId);

            if (pedido == null)
            {
                return NotFound($"No se encontró un pedido para el usuario con ID {userId}.");
            }
            return Ok(pedido);
        }



        [HttpPost]
        [Route("AddPedido")]
        public IActionResult AddPedido(PedidosEncabezados pedido)
        {
            // Asegurarse de que Entity Framework conoce que el usuario no debe ser creado
            if (pedido.Usuario != null && pedido.Usuario.IdUsuario > 0)
            {
                _context.Entry(pedido.Usuario).State = EntityState.Unchanged;
            }

            _context.PedidoEncabezado.Add(pedido);
            _context.SaveChanges();
            return Ok(pedido);
        }

        [HttpPut]
        [Route("UpdatePedido/{id}")]
        public IActionResult UpdatePedido(int id, PedidosEncabezados pedidoModificado)
        {
            var pedido = _context.PedidoEncabezado.FirstOrDefault(p => p.IdPedido == id);

            if (pedido == null)
            {
                return NotFound();
            }

            if (id != pedidoModificado.IdPedido)
            {
                return BadRequest("El ID del pedido no coincide con el ID en la URL.");
            }

            // Actualizar solo el campo de estatus
            pedido.Estatus = pedidoModificado.Estatus;

            _context.SaveChanges();
            return Ok(pedido);
        }


        [HttpPost]
        [Route("AddPedidoDetalle")]
        public IActionResult AddPedidoDetalle(PedidosDetalles pedidoDetalle)
        {
            // Marcar el alimento como no modificado para evitar que EF intente crear uno nuevo
            if (pedidoDetalle.Alimento != null && pedidoDetalle.Alimento.IdAlimento > 0)
            {
                _context.Entry(pedidoDetalle.Alimento).State = EntityState.Unchanged;
            }

            // Opcional: si también estás pasando el objeto Pedido y no quieres crear uno nuevo
            if (pedidoDetalle.Pedido != null && pedidoDetalle.Pedido.IdPedido > 0)
            {
                _context.Entry(pedidoDetalle.Pedido).State = EntityState.Unchanged;
            }

            _context.PedidoDetalle.Add(pedidoDetalle);
            _context.SaveChanges();
            return Ok(pedidoDetalle);
        }

        [HttpGet]
        [Route("GetDetallePedido/{IdPedido}")]
        public IActionResult GetDetallePedido(int IdPedido)
        {
            var detalles = _context.PedidoDetalle
                                  .Include(pd => pd.Alimento)
                                  .Where(pd => pd.IdPedido == IdPedido)
                                  .ToList();

            if (detalles.Count == 0)
            {
                return NotFound($"No se encontraron detalles para el pedido con ID {IdPedido}.");
            }
            return Ok(detalles);
        }


    }
}
