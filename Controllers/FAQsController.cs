using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FAQsController
    {
        private readonly TecApiContext _context;

        public FAQsController(TecApiContext context)
        {
            _context = context;
        }
    }
}
