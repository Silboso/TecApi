using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredencialesController
    {
        private readonly TecApiContext _context;

        public CredencialesController(TecApiContext context)
        {
            _context = context;
        }
    }
}
