using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecApi.Models
{
    public class Categorias
    {
        public int Id { get; set; }
        [EnumDataType(typeof(CategoriaEnum))]
        public CategoriaEnum Categoria { get; set; }
    }

    public enum CategoriaEnum
    {
        Educación,
        Tecnología,
        Salud,
        Entretenimiento,
        Alimentación,
        Transporte,
        Viajes
    }

}
