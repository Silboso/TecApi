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

        [Column("marca")]
        public string Marca { get; set; }

        [Column("modelo")]
        public string Modelo { get; set; }

        [Column("color")]
        public string Color { get; set; }

        [Column("status")]
        public bool Status { get; set; }
    }
}
