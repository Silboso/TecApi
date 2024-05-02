using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("valoraciones")]
    public class Valoraciones
    {
        [Key]
        [Column("idvaloracion")]
        public int IdValoracion { get; set; }

        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("idalimento")]
        public int IdAlimento { get; set; }

        [Column("valor")]
        public int Valor { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }

        [ForeignKey("IdAlimento")]
        public Alimentos Alimento { get; set; }
    }
}
