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

        [Column("idetiqueta")]
        public int IdEtiqueta { get; set; }

        [ForeignKey("IdAviso")]
        public Avisos Aviso { get; set; }

        [ForeignKey("IdEtiqueta")]
        public Etiquetas Etiqueta { get; set; }
    }
}
