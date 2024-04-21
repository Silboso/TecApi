using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConductoresController
    {
        private readonly TecApiContext _context;

        public ConductoresController(TecApiContext context)
        {
            _context = context;
        }
    }
}
