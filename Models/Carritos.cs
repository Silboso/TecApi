using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("carrito")]
    public class Carritos
    {
        [Key]
        [Column("idcarrito")]
        public int IdCarrito { get; set; }

        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }

    }
}
