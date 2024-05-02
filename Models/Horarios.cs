using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("horario")]
    public class Horarios
    {
        [Key]
        [Column("idhorario")]
        public int IdHorario { get; set; }

        [Column("iddirectorio")]
        public int IdDirectorio { get; set; }

        [Column("dia")]
        public string Dia { get; set; }

        [Column("hsalida")]
        public TimeSpan HSalida { get; set; }

        [Column("hregreso")]
        public TimeSpan HRegreso { get; set; }

        [ForeignKey("IdDirectorio")]
        public Directorios Directorio { get; set; }
    }
}
