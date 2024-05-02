using Microsoft.EntityFrameworkCore;
using Npgsql;
using TecApi.Models;


namespace TecApi.Context
{
    public class TecApiContext : DbContext
    {
        public DbSet<Alimentos> Alimento { get; set; }

        public DbSet<Avisos> Aviso { get; set; }

        public DbSet<AvisosEtiquetas> AvisoEtiqueta { get; set; }

        public DbSet<Beneficios> Beneficio { get; set; }

        public DbSet<Carritos> Carrito { get; set; }

        public DbSet<CarritoDetalle> CarritoDetalle { get; set; }

        public DbSet<CategoriasAlimentos> CategoriaAlimento { get; set; }

        public DbSet<CategoriasBeneficios> CategoriaBeneficio { get; set; }

        public DbSet<Comentarios> Comentario { get; set; }

        public DbSet<Conductores> Conductor { get; set; }

        public DbSet<Credenciales> Credencial { get; set; }

        public DbSet<Directorios> Directorio { get; set; }

        public DbSet<Etiquetas> Etiqueta { get; set; }

        public DbSet<FAQs> FAQ { get; set; }

        public DbSet<Horarios> Horario { get; set; }

        public DbSet<PedidosDetalles> PedidoDetalle { get; set; }

        public DbSet<PedidosEncabezados> PedidoEncabezado { get; set; }

        public DbSet<Pines> Pin { get; set; }

        public DbSet<Valoraciones> Valoracion { get; set; }

        public DbSet<Usuarios> Usuario { get; set; }

        public TecApiContext(DbContextOptions<TecApiContext> options) : base(options) { }

    }
}
