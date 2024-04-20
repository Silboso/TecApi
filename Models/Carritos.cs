using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("carrito")]
    public class Carritos
    {
        [Key]
        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("idalimento")]
        public int IdAlimento { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("total")]
        public decimal Total { get; set; }
    }
}
