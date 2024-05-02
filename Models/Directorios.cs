using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TecApi.Models
{
    [Table("directorio")]
    public class Directorios
    {
        [Key]
        [Column("iddirectorio")]
        public int IdDirectorio { get; set; }

        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("marcaa")]
        public string Marca { get; set; }

        [Column("modeloa")]
        public string Modelo { get; set; }

        [Column("colora")]
        public string Color { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }
    }
}
