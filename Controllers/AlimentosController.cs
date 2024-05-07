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
        public IActionResult GetAllAlimentos()
        {
            // Obtiene todos los alimentos de la base de datos
            var alimentos = _context.Alimento
                                     .Include(a => a.Categoria)  // Incluye la categoría para cada alimento
                                     .ToList();

            // Verifica si la lista está vacía
            if (!alimentos.Any())
            {
                return NotFound("No se encontraron alimentos.");
            }

            // Devuelve la lista de alimentos
            return Ok(alimentos);
        }

        [HttpGet]
        [Route("GetAllAlimentosActivos")]
        public IEnumerable<Alimentos> GetAllAlimentosActivos()
        {
            // Filtrar los alimentos para devolver solo aquellos cuyo estado es TRUE
            return _context.Alimento
                           .Include(a => a.Categoria) // Incluye la categoría correspondiente
                           .Where(a => a.Estado == true) // Filtra por el estado
                           .ToList(); // Convierte los resultados en una lista
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
        [Route("z")]
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
            alimentoActual.Url = alimento.Url;
            alimentoActual.Nombre = alimento.Nombre;
            alimentoActual.Precio = alimento.Precio;
            alimentoActual.Descripcion = alimento.Descripcion;
            alimentoActual.IdCategoria = alimento.IdCategoria;


            // Guarda los cambios
            _context.SaveChanges();

            // Devuelve el alimento actualizado
            return Ok(alimentoActual);
        }

        [HttpPut]
        [Route("PutEstadoAlimento/{id}")]
        public IActionResult PutEstadoAlimento(int id)
        {
            // Busca el alimento correspondiente en la base de datos
            var alimento = _context.Alimento.FirstOrDefault(a => a.IdAlimento == id);

            // Verifica si el alimento fue encontrado
            if (alimento == null)
            {
                return NotFound($"No se encontró el alimento con el ID: {id}.");
            }

            // Cambia el estado del alimento a FALSE
            alimento.Estado = false;

            // Guarda los cambios en la base de datos
            _context.SaveChanges();

            // Retorna una respuesta indicando que la operación fue exitosa
            return Ok($"El estado del alimento con ID {id} ha sido actualizado a inactivo.");

        }

        [HttpPut]
        [Route("PutActivarAlimento/{id}")]
        public IActionResult PutActivarAlimento(int id)
        {
            // Busca el alimento correspondiente en la base de datos
            var alimento = _context.Alimento.FirstOrDefault(a => a.IdAlimento == id);

            // Verifica si el alimento fue encontrado
            if (alimento == null)
            {
                return NotFound($"No se encontró el alimento con el ID: {id}.");
            }

            // Cambia el estado del alimento a TRUE
            alimento.Estado = true;

            // Guarda los cambios en la base de datos
            _context.SaveChanges();

            // Retorna una respuesta indicando que la operación fue exitosa
            return Ok($"El estado del alimento con ID {id} ha sido actualizado a activo.");
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
