using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeneficiosController : ControllerBase
    {
        private readonly TecApiContext _context;

        public BeneficiosController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetBeneficios")]
        public IEnumerable<Beneficios> GetBeneficios()
        {
            return _context.Beneficio.Include(b => b.CategoriaBeneficio).ToList();
        }

        [HttpGet]
        [Route("GetBeneficioById/{id}")]
        public IActionResult GetBeneficioById(int id)
        {
            var beneficio = _context.Beneficio.Find(id);
            if (beneficio == null)
            {
                return NotFound();
            }

            return Ok(beneficio);
        }

        [HttpPost]
        [Route("PostBeneficio")]
        public async Task<IActionResult> PostBeneficio(Beneficios beneficio)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            CategoriasBeneficios categoria = (await _context.CategoriaBeneficio.FindAsync(beneficio.IdCategoriaBeneficio))!;
            
            beneficio.CategoriaBeneficio = categoria;
            
            _context.Beneficio.Add(beneficio);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetBeneficioById), new { id = beneficio.IdBeneficio }, beneficio);
        }

        [HttpPut]
        [Route("PutBeneficio/{id}")]
        public IActionResult PutBeneficio(int id, Beneficios beneficio)
        {
            if (id != beneficio.IdBeneficio)
            {
                return BadRequest();
            }

            _context.Entry(beneficio).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }
        
        [HttpDelete]
        [Route("DeleteBeneficio/{id}")]
        public IActionResult DeleteBeneficio(int id)
        {
            var beneficio = _context.Beneficio.Find(id);
            if (beneficio == null)
            {
                return NotFound();
            }

            _context.Beneficio.Remove(beneficio);
            _context.SaveChanges();
            return NoContent();
        }

        ////////////////////////
        ////////////////////////
        ////// Categorias //////
        ////////////////////////
        ////////////////////////
        
        [HttpGet]
        [Route("GetCategorias")]
        public IEnumerable<CategoriasBeneficios> GetCategorias()
        {
            return _context.CategoriaBeneficio.ToList();
        }
    }
}