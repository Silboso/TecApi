using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("beneficios")]
    public class Beneficios
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("titulo")]
        public string Titulo { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("imagen")]
        public string Imagen { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("categoria")]
        public Categorias Categoria { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [ForeignKey("Categoria")]
        public Categorias Categorias { get; set; }
    }
}
