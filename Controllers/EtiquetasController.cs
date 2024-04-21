using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EtiquetasController
    {
        private readonly TecApiContext _context;

        public EtiquetasController(TecApiContext context)
        {
            _context = context;
        }
    }
}
