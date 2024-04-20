using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TecApi.Models
{
    [Table("alimentos")]
    public class Alimentos
    {
        [Key]
        [Column("idalimento")]
        public int IdAlimento { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("idcategoria")]
        public int IdCategoria { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("idvaloracion")]
        public int IdValoracion { get; set; }
    }
}
