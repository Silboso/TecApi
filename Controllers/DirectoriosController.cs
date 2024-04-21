using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DirectoriosController
    {
        private readonly TecApiContext _context;

        public DirectoriosController(TecApiContext context)
        {
            _context = context;
        }
    }
}
