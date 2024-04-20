using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TecApi.Models
{

    [Table("usuarios")]
    public class ModeloUsuario
    {
        [Key]
        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("foto")]
        public string Foto { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellidopaterno")]
        public string ApellidoPaterno { get; set; }

        [Column("apellidomaterno")]
        public string ApellidoMaterno { get; set; }

        [Column("matricula")]
        public int Matricula { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("carrera")]
        public string Carrera { get; set; }

        [Column("semestre")]
        public string Semestre { get; set; }

        [Column("sexo")]
        public bool Sexo { get; set; }

        [Column("rol")]
        public int Rol { get; set; }
    }
}
