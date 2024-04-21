using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PinesController : ControllerBase
    {
        private readonly TecApiContext _context;

        public PinesController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllPines")]
        public IEnumerable<Pines> GetAllPines()
        {
            return _context.Pin.ToList();
        }
    }
}
