using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("avisos")]
    public class Avisos
    {
        [Key]
        [Column("idaviso")]
        public int IdAviso { get; set; }

        [Column("titulo")]
        public string Titulo { get; set; }

        [Column("contenido")]
        public string Contenido { get; set; }

        [Column("fechacreacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("imagen")]
        public string Imagen { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }

        [ForeignKey("idetiqueta")]
        public Etiquetas Etiqueta { get; set; }
    }
}
