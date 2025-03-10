﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("GetCarritoByUserId/{userId}")]
        public async Task<ActionResult<Carritos>> GetCarritoByUserId(int userId)
        {
            var carrito = await _context.Carrito.FirstOrDefaultAsync(c => c.IdUsuario == userId);
            if (carrito == null)
            {
                return NotFound();
            }
            return Ok(carrito);
        }

        [HttpPost]
        [Route("AddCarrito")]
        public async Task<ActionResult<Carritos>> AddCarrito(Carritos carrito)
        {

            carrito.Usuario = null;

            _context.Carrito.Add(carrito);

            await _context.SaveChangesAsync();

            return Ok(carrito);
        }


 [HttpPost]
[Route("AddCarritoDetalle")]
public async Task<ActionResult<CarritoDetalle>> AddCarritoDetalle(CarritoDetalle carritoDetalle)
{
    try
    {
        // Verificar si el carrito existe
        var carritoExistente = await _context.Carrito.FindAsync(carritoDetalle.IdCarrito);
        if (carritoExistente == null)
        {
            return BadRequest($"El carrito con ID {carritoDetalle.IdCarrito} no existe.");
        }

        // Verificar si el alimento existe
        var alimentoExistente = await _context.Alimento.FindAsync(carritoDetalle.IdAlimento);
        if (alimentoExistente == null)
        {
            return BadRequest($"El alimento con ID {carritoDetalle.IdAlimento} no existe.");
        }

        // Buscar un detalle de carrito existente con el mismo alimento
        var detalleExistente = await _context.CarritoDetalle
            .FirstOrDefaultAsync(cd => cd.IdCarrito == carritoDetalle.IdCarrito && cd.IdAlimento == carritoDetalle.IdAlimento);
        
        if (detalleExistente != null)
        {
            // Si el detalle ya existe, no agregamos un nuevo detalle
            return BadRequest($"El alimento con ID {carritoDetalle.IdAlimento} ya está en el carrito.");
        }

        // Si no existe, agregamos un nuevo detalle
        carritoDetalle.Carrito = carritoExistente;
        carritoDetalle.Alimento = alimentoExistente;
        _context.CarritoDetalle.Add(carritoDetalle);

        await _context.SaveChangesAsync();

        return Ok(carritoDetalle);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error al agregar detalle de carrito: {ex.Message}");
    }
}


        [HttpDelete]
        [Route("DeleteAllCarritoDetalle/{idCarrito}")]
        public async Task<ActionResult> DeleteAllCarritoDetalle(int idCarrito)
        {
            var detallesCarrito = await _context.CarritoDetalle.Where(cd => cd.IdCarrito == idCarrito).ToListAsync();
            if (!detallesCarrito.Any())
            {
                return NotFound($"No se encontraron detalles para el carrito con ID {idCarrito}.");
            }

            _context.CarritoDetalle.RemoveRange(detallesCarrito);
            await _context.SaveChangesAsync();
            return Ok($"Todos los detalles del carrito con ID {idCarrito} han sido eliminados correctamente.");
        }

[HttpDelete]
[Route("DeleteCarritoDetalle/{idDetalle}")]
public async Task<ActionResult> DeleteCarritoDetalle(int idDetalle)
{
    var detalleCarrito = await _context.CarritoDetalle.FindAsync(idDetalle);
    if (detalleCarrito == null)
    {
        return NotFound($"Detalle de carrito con ID {idDetalle} no encontrado.");
    }

    _context.CarritoDetalle.Remove(detalleCarrito);
    await _context.SaveChangesAsync();
    return Ok($"Detalle de carrito con ID {idDetalle} eliminado correctamente.");
}


        [HttpGet]
        [Route("GetCarritoDetalle/{carritoId}")]
        public async Task<ActionResult<IEnumerable<CarritoDetalle>>> GetCarritoDetalle(int carritoId)
        {
            var carritoDetalles = await _context.CarritoDetalle
                                                .Where(cd => cd.IdCarrito == carritoId)
                                                .Include(cd => cd.Alimento)  // Asegura que también se cargue el alimento
                                                .ToListAsync();

            if (carritoDetalles == null || carritoDetalles.Count == 0)
            {
                return NotFound($"No se encontraron detalles para el carrito con ID: {carritoId}");
            }

            return Ok(carritoDetalles);
        }


    }
}
