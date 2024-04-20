using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("comentarios")]
    public class Comentarios
    {
        [Key]
        [Column("idcomentario")]
        public int IdComentario { get; set; }

        [Column("contenido")]
        public string Contenido { get; set; }

        [Column("fechacreacion")]
        public string FechaCreacion { get; set; }

        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("idaviso")]
        public int IdAviso { get; set; }
    }
}
