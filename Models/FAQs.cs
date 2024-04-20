using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    [Table("faq")]
    public class FAQs
    {
        [Key]
        [Column("idpregunta")]
        public int IdPregunta { get; set; }

        [Column("pregunta")]
        public string Pregunta { get; set; }

        [Column("respuesta")]
        public string Respuesta { get; set; }
    }
}
