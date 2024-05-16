using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("pines")]
    public class Pines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idpin")]
        public int IdPin { get; set; }

        [Column("iddirectorio")]
        public int IdDirectorio { get; set; }

        [Column("coordenadax")]
        public string CoordenadaX { get; set; }

        [Column("coordenaday")]
        public string CoordenadaY { get; set; }

        [ForeignKey("IdDirectorio")]
        public Directorios Directorio { get; set; }
    }
}
