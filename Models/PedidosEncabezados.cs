using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("pedidosencabezado")]
    public class PedidosEncabezados
    {
        [Key]
        [Column("idpedido")]
        public int IdPedido { get; set; }

        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("estatus")]
        public string Estatus { get; set; }

        [Column("total")]
        public decimal Total { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }
    }
}
