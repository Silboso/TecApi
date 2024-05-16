using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("horario")]
    public class Horarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idhorario")]
        public int IdHorario { get; set; }

        [Column("iddirectorio")]
        public int IdDirectorio { get; set; }

        [Column("dia")]
        public string Dia { get; set; }

        [Column("hsalida")]
        public String HSalida { get; set; }

        [Column("hregreso")]
        public String HRegreso { get; set; }

        [ForeignKey("IdDirectorio")]
        public Directorios Directorio { get; set; }


    }
}
