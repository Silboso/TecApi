using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    /*CREATE TABLE Credenciales (
    Id INT NOT NULL,
    IdUsuario INT NOT NULL REFERENCES Usuarios(IdUsuario),
    Serial VARCHAR(255) NOT NULL UNIQUE
);*/
    [Table("credenciales")]
    public class Credenciales
    {
        [Key]
        [Column("idcredencial")]
        public int Id { get; set; }

        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("serial")]
        public string Serial { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }
    }
}
