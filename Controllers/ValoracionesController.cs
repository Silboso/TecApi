using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecApi.Context;
using TecApi.Models;

namespace TecApi.Controllers
{
    public class ValoracionesController
    {
        private readonly TecApiContext _context;

        public ValoracionesController(TecApiContext context)
        {
            _context = context;
        }
    }
}
