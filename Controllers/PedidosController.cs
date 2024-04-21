using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController
    {
        private readonly TecApiContext _context;

        public PedidosController(TecApiContext context)
        {
            _context = context;
        }
    }
}
