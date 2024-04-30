using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConductoresController : ControllerBase
    {
        private readonly TecApiContext _context;

        public ConductoresController(TecApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllConductores")]
        public IEnumerable<Conductores> GetAllConductores()
        {
            return _context.Conductor.ToList();
        }

        

    }
}
