using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Category
    {
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esste campo deve ter enter 3 e 60 chars")]
        [MinLength(3, ErrorMessage = "Esste campo deve ter enter 3 e 60 chars")]
        public string Title {get; set;}
    }
}