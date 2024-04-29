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
        [Route("GetAllCarritos")]
        public IEnumerable<Carritos> GetAllCarritos()
        {
            return _context.Carrito.Include(c => c.Alimento).ToList();
        }

        [HttpGet]
        [Route("GetCarrito/{id}")]
        public async Task<ActionResult<Carritos>> GetCarrito(int id)
        {
            var carrito = await _context.Carrito.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            return Ok(carrito);
        }

        [HttpGet]
        [Route("AddCarrito")]
        public async Task<ActionResult<Carritos>> AddCarrito(Carritos carrito)
        {
            _context.Carrito.Add(carrito);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCarrito", new { id = carrito.IdUsuario }, carrito);
        }
    }
}
