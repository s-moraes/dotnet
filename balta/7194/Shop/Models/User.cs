using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class User
    {
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve ter entre 20 e 3 chars")]
        [MinLength(3, ErrorMessage = "Este campo deve ter entre 20 e 3 chars")]
        public string Username {get; set;}

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve ter entre 20 e 3 chars")]
        [MinLength(3, ErrorMessage = "Este campo deve ter entre 20 e 3 chars")]
        public string Password {get; set;}

        public string Role {get; set;}
    }
}
