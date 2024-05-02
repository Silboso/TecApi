using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FAQsController : ControllerBase
    {
        private readonly TecApiContext _context;

        public FAQsController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllFAQs")]
        public IEnumerable<FAQs> GetAllFAQs()
        {
            return _context.FAQ.ToList();
        }

        [HttpGet]
        [Route("GetFAQsId")]
        public IEnumerable<FAQs> GetFAQsId()
        {
            //Solo regresa los id de los FAQs
            return _context.FAQ.Select(a => new FAQs { IdPregunta = a.IdPregunta }).ToList();
        }

        [HttpGet]
        [Route("GetFAQ/{id}", Name = "GetFAQ")]
        public IActionResult GetFAQByID(int id)
        {
            var faq = _context.FAQ.FirstOrDefault(a => a.IdPregunta == id);
            if (faq == null)
            {
                return NotFound();
            }
            return Ok(faq);
        }
    }
}
