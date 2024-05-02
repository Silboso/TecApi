using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("carrito_detalle")]
    public class CarritoDetalle
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idCarrito")]
        public int IdCarrito { get; set; }

        [Column("idAlimento")]
        public int IdAlimento { get; set; }

        [ForeignKey("IdCarrito")]
        public Carritos Carrito { get; set; }

        [ForeignKey("IdAlimento")]
        public Alimentos Alimento { get; set; }
    }
}
