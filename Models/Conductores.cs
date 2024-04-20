using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("conductores")]
    public class Conductores
    {
        [Key]
        [Column("idconductor")]
        public int IdConductor { get; set; }

        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }
    }
}
