using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace TecApi.Models
{
    [Table("credenciales")]
    public class Credenciales
    {
        [Key]
        [Column("idcredencial")]
        public int IdCredencial { get; set; }

        [Column("idusuario")]
        [Required]
        public int IdUsuario { get; set; }

        [Column("serial")]
        public string? Serial { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios? Usuario { get; set; }
    }
}
