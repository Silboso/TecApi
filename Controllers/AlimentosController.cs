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
            // Incluye la carga de la categoría correspondiente
            var alimento = _context.Alimento.Include(a => a.Categoria).FirstOrDefault(a => a.IdAlimento == id);
            if (alimento == null)
            {
                // Si no se encuentra el alimento, devuelve un error
                return NotFound();
            }
            // Si se encuentra el alimento, lo devuelve
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

        [HttpPut]
        [Route("PutAlimento/{id}")]
        public IActionResult PutAlimento(int id, Alimentos alimento)
        {
            // Busca el alimento correspondiente
            var alimentoActual = _context.Alimento.Find(id);
            if (alimentoActual == null)
            {
                // Si el alimento no existe, devuelve un error
                return NotFound();
            }

            // Actualiza los campos del alimento
            alimentoActual.Nombre = alimento.Nombre;
            alimentoActual.Precio = alimento.Precio;
            alimentoActual.IdCategoria = alimento.IdCategoria;

            // Guarda los cambios
            _context.SaveChanges();

            // Devuelve el alimento actualizado
            return Ok(alimentoActual);
        }

        [HttpGet]
        [Route("GetAlimentosByCategoria/{id}")]
        public IActionResult GetAlimentosByCategoria(int id)
        {
            // Busca la categoría correspondiente
            var categoria = _context.CategoriaAlimento.Find(id);
            if (categoria == null)
            {
                // Si la categoría no existe, devuelve un error
                return NotFound("La categoría especificada no existe.");
            }
            // Busca los alimentos que pertenecen a la categoría
            var alimentos = _context.Alimento.Where(a => a.IdCategoria == id).ToList();
            return Ok(alimentos);
        }

        [HttpGet]
        [Route("GetAlimentosBeLike/{nombre}")]
        public IActionResult GetAlimentosBeLike(string nombre)
        {
            // Busca los alimentos que contienen el nombre especificado
            var alimentos = _context.Alimento.Where(a => a.Nombre.Contains(nombre)).ToList();
            return Ok(alimentos);
        }

        [HttpPost]
        [Route("PostCategoria")]
        public IActionResult PostCategoria(CategoriasAlimentos categoria)
        {
            // Agrega la categoría al contexto y guarda los cambios
            _context.CategoriaAlimento.Add(categoria); // Cambio aquí
            _context.SaveChanges();

            // Devuelve la categoría creada junto con un código 201 (Created)
            return Ok(categoria);
        }

        [HttpGet]
        [Route("GetAllCategorias")]
        public IEnumerable<CategoriasAlimentos> GetAllCategorias()
        {
            // Devuelve todas las categorías
            return _context.CategoriaAlimento.ToList();
        }
    }
}
