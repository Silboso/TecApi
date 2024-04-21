using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("categoriabeneficio")]
    public class CategoriasBeneficios
    {
        [Key]
        [Column("idcategoriabeneficio")]
        public int IdCategoriaBeneficio { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
