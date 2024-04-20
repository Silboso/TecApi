using Microsoft.EntityFrameworkCore;
using TecApi.Models;


namespace TecApi.Context
{
    public class TecApiContext : DbContext
    {
        public DbSet<ModeloUsuario> Usuarios { get; set; }

        public TecApiContext(DbContextOptions<TecApiContext> options) : base(options) { }

    }
}
