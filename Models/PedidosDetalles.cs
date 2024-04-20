using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("pedidosdetalle")]
    public class PedidosDetalles
    {
        [Key]
        [Column("idpedido")]
        public int IdPedido { get; set; }

        [Column("idalimento")]
        public int IdAlimento { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("precio")]
        public decimal Precio { get; set; }

        [ForeignKey("IdAlimento")]
        public Alimentos Alimento { get; set; }

        [ForeignKey("IdPedido")]
        public PedidosEncabezados Pedido { get; set; }
    }
}
