using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("avisosetiquetas")]
    public class AvisosEtiquetas
    {
        [Key]
        [Column("idaviso")]
        public int IdAviso { get; set; }

        [Key]
        [Column("idetiqueta")]
        public int IdEtiqueta { get; set; }
    }
}
