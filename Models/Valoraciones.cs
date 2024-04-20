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

        [Column("valor")]
        public int Valor { get; set; }
    }
}
