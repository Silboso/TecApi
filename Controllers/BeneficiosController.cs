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
        [Route("GetAllBeneficios")]
        public IEnumerable<Beneficios> GetAllBeneficios()
        {
            return _context.Beneficio.Include(b => b.CategoriaBeneficio).ToList();
        }

        [HttpGet]
        [Route("GetBeneficio/{id}", Name = "GetBeneficio")]
        public IActionResult GetBeneficioByID(int id)
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
        public IActionResult PostBeneficio(Beneficios beneficio)
        {
            _context.Beneficio.Add(beneficio);
            _context.SaveChanges();
            return CreatedAtRoute("GetBeneficio", new { id = beneficio.IdBeneficio }, beneficio);
        }
    }
}
