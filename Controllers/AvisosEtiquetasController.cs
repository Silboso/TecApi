using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvisosEtiquetasController
    {
        private readonly TecApiContext _context;

        public AvisosEtiquetasController(TecApiContext context)
        {
            _context = context;
        }
    }
}
