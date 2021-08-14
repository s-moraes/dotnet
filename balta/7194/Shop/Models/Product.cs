using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Product
    {
        [Key]
        public int Id {get; set;}
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve ter entre 60 e 3 chars")]
        [MinLength(3, ErrorMessage = "Este campo deve ter entre 60 e 3 chars")]
        public string Title {get; set;}

        [MaxLength(1024, ErrorMessage = "Esse campo deve ter no max 1024 chars")]
        public string Description {get; set;}

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Esse campo deve maior que zero")]
        public decimal Price {get; set;}

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
        public int CategoryId {get; set;}

        public Category Category {get; set;}
    }
}
