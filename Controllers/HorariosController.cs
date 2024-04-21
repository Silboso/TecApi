using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HorariosController
    {
        private readonly TecApiContext _context;

        public HorariosController(TecApiContext context)
        {
            _context = context;
        }
    }
}
