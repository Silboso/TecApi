using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlimentosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public AlimentosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllAlimentos")]
        public IEnumerable<Alimentos> GetAllAlimentos()
        {
            // Incluye la carga de la categoría correspondiente
            return _context.Alimento.Include(a => a.Categoria).ToList();
        }

        [HttpGet]
        [Route("GetAlimento/{id}", Name = "GetAlimento")]
        public IActionResult GetAlimentoByID(int id)
        {
            var alimento = _context.Alimento.Include(a => a.Categoria).FirstOrDefault(a => a.IdAlimento == id);
            if (alimento == null)
            {
                return NotFound();
            }
            return Ok(alimento);
        }

        [HttpPost]
        [Route("PostAlimento")]
        public IActionResult PostAlimento(Alimentos alimento)
        {
            // Antes de agregar el alimento, busca la categoría correspondiente por su ID
            var categoria = _context.CategoriaAlimento.Find(alimento.IdCategoria);
            if (categoria == null)
            {
                // Si la categoría no existe, devuelve un error
                return BadRequest("La categoría especificada no existe.");
            }

            // Asigna la categoría al alimento
            alimento.Categoria = categoria;

            // Agrega el alimento al contexto y guarda los cambios
            _context.Alimento.Add(alimento);
            _context.SaveChanges();

            // Devuelve el alimento creado junto con un código 201 (Created)
            return CreatedAtRoute("GetAlimento", new { id = alimento.IdAlimento }, alimento);
        }
    }
}
