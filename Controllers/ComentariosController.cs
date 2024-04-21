using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComentariosController
    {
        private readonly TecApiContext _context;

        public ComentariosController(TecApiContext context)
        {
            _context = context;
        }
    }
}
