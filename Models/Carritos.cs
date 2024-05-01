using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("carrito")]
    public class Carritos
    {
        [Key]
        [Column("idCarrito")]
        public int IdCarrito { get; set; }

        [Column("idUsuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }

    }
}
