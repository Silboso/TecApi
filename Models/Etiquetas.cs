using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("etiquetas")]
    public class Etiquetas
    {
        [Key]
        [Column("idetiqueta")]
        public int IdEtiqueta { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
